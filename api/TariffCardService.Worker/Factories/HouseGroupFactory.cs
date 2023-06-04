using System.Collections.Generic;
using System.Linq;

using TariffCardService.Core.Enum;
using TariffCardService.Core.Models;
using TariffCardService.Worker.Helpers;

namespace TariffCardService.Worker.Factories
{
	/// <summary>
	/// Методы создания и обработки объектов с данными о корпусе.
	/// </summary>
	public static class HouseGroupFactory
	{
		/// <summary>
		/// Создание объекта с данными о корпусе.
		/// </summary>
		/// <param name="house"> Данные о корпусе.</param>
		/// <returns> Объект с данными о корпусе.</returns>
		public static HouseGroup Create(HouseHelper house)
		{
			var objectGroups = house.ObjectsInHouse.Select(ObjectGroupFactory.Create).ToArray();
			var commission = GetCommissionRangeValueInHouse(objectGroups);
			var objectsGrouping = ObjectGroupFactory
				.GroupingObject(objectGroups
					.Where(x =>
						x.CommissionType != house.CommissionType ||
						x.CommissionValue != house.CommissionValue ||
						x.ApartmentId != null)
					.OrderBy(x => x.Rooms)
					.ThenByDescending(x => x.ApartmentDescription)
					.ToArray());

			CommissionType? houseCommissionType;
			decimal? houseCommissionValue;
			int objectsGroupingCount;
			bool hasOverriding;

			if (objectsGrouping.All(x =>
				    x.CommissionType == house.SellerCommissionType &&
				    x.CommissionValue == house.SellerCommissionValue &&
				    x.ApartmentId == null) && house.CommissionValue == null)
			{
				houseCommissionType = house.SellerCommissionType;
				houseCommissionValue = house.SellerCommissionValue;
				objectsGroupingCount = 0;
				hasOverriding = false;
				objectsGrouping = new List<ObjectGroup>();
			}
			else
			{
				houseCommissionType = house.CommissionType;
				houseCommissionValue = house.CommissionValue;
				objectsGroupingCount = objectsGrouping.Count;
				hasOverriding = objectsGrouping.Any(x => x.IsOverriding);
			}

			var model = new HouseGroup
			{
				HouseId = house.HouseId,
				HouseName = house.HouseName,
				RealtyObjectType = house.RealtyObjectType,
				CommissionType = houseCommissionType,
				CommissionValue = houseCommissionValue,
				ObjectGroups = objectsGrouping,
				MinMaxCommissionType = commission.CommissionType,
				MaxCommissionValue = commission.MaxCommissionValue,
				MinCommissionValue = commission.MinCommissionValue,
				ObjectsCount = objectsGroupingCount,
				CrossRegionAdvancedBookingCoefficient = house.CrossRegionAdvancedBookingCoefficient,
				HasOverriding = hasOverriding,
			};

			return model;
		}

		/// <summary>
		/// Группировка корпусов по общим параметрам при совпадении всех их атрибутов.
		/// </summary>
		/// <param name="housesGroups"> Данные о корпусах, требующих группировки.</param>
		/// <returns> Коллекция сгруппированных корпусов.</returns>
		public static ICollection<HouseGroup> GroupingHouses(ICollection<HouseGroup> housesGroups)
		{
			var housesGrouping = new List<HouseGroup>();

			foreach (var house in housesGroups)
			{
				if (housesGrouping.All(h => h.CommissionValue != house.CommissionValue ||
				                            h.CommissionType != house.CommissionType ||
				                            h.MinMaxCommissionType != house.MinMaxCommissionType ||
				                            h.MaxCommissionValue != house.MaxCommissionValue ||
				                            h.MinCommissionValue != house.MinCommissionValue ||
				                            h.ObjectsCount != house.ObjectsCount ||
				                            h.CrossRegionAdvancedBookingCoefficient !=
				                            house.CrossRegionAdvancedBookingCoefficient ||
				                            h.HasOverriding != house.HasOverriding))
				{
					var similarHouses = housesGroups
						.Where(x =>
							x.ObjectGroups
								.Select(h => new
								{
									h.Rooms,
									h.ApartmentDescription,
									h.CommissionType,
									h.CommissionValue,
									h.ApartmentId,
								})
								.SequenceEqual(house.ObjectGroups
									.Select(h => new
									{
										h.Rooms,
										h.ApartmentDescription,
										h.CommissionType,
										h.CommissionValue,
										h.ApartmentId,
									})) &&
							x.CommissionType == house.CommissionType &&
							x.CommissionValue == house.CommissionValue &&
							x.MaxCommissionValue == house.MaxCommissionValue &&
							x.MinCommissionValue == house.MinCommissionValue &&
							x.MinMaxCommissionType == house.MinMaxCommissionType &&
							x.CrossRegionAdvancedBookingCoefficient == house.CrossRegionAdvancedBookingCoefficient &&
							x.ObjectsCount == house.ObjectsCount &&
							x.HasOverriding == house.HasOverriding)
						.ToArray();

					housesGrouping.Add(new HouseGroup
					{
						HouseId = similarHouses.Length == 1 ? house.HouseId : null,
						HouseName = string.Join(", ", similarHouses.Select(x => x.HouseName)),
						RealtyObjectType = house.RealtyObjectType,
						CommissionType = house.CommissionType,
						CommissionValue = house.CommissionValue,
						ObjectGroups = house.ObjectGroups,
						MinMaxCommissionType = house.MinMaxCommissionType,
						MaxCommissionValue = house.MaxCommissionValue,
						MinCommissionValue = house.MinCommissionValue,
						ObjectsCount = house.ObjectsCount,
						CrossRegionAdvancedBookingCoefficient = house.CrossRegionAdvancedBookingCoefficient,
						HasOverriding = house.HasOverriding,
					});
				}
			}

			return housesGrouping;
		}

		/// <summary>
		/// Определение минимального и максимального значения и типа комиссии субагента на помещения в корпусе.
		/// </summary>
		/// <param name="currentObjectGroups"> Данные об объектах в комплексе.</param>
		/// <returns> Минимальное и максимальное значение и тип комиссии субагента на помещения в корпусе.</returns>
		private static CommissionRangeHelpers GetCommissionRangeValueInHouse(ObjectGroup[] currentObjectGroups)
		{
			var commissionTypeGroups = currentObjectGroups
				.GroupBy(x => x.CommissionType)
				.OrderByDescending(x => x.Key == CommissionType.Percent)
				.ThenByDescending(x => x.Key == CommissionType.AbsolutePerSqMeter)
				.ThenByDescending(x => x.Key == CommissionType.Absolute);
			var commissionGroup = commissionTypeGroups.First();
			var commissions = commissionGroup.ToList();

			return new CommissionRangeHelpers
			{
				CommissionType = commissionGroup.Key,
				MaxCommissionValue = commissions.Max(x => x.CommissionValue),
				MinCommissionValue = commissions.Min(x => x.CommissionValue),
			};
		}
	}
}