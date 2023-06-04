using System;
using System.Collections.Generic;
using System.Linq;

using TariffCardService.Core.Enum;
using TariffCardService.Core.Models;
using TariffCardService.Worker.Helpers;

namespace TariffCardService.Worker.Factories
{
	/// <summary>
	/// Методы создания и обработки объектов с данными о комплексе и его продавце.
	/// </summary>
	public static class ComplexFactory
	{
		/// <summary>
		/// Создание объекта с данными о комплексе и его продавце.
		/// </summary>
		/// <param name="complex">Исходные данные о комплексе и его продавце.</param>
		/// <param name="conditionsOfPaymentFees"> Условиях выплат вознаграждения по компании-продавца.</param>
		/// <param name="isSellerCommissionPrepayments"> Данные о разрешении расширенного авансирования.</param>
		/// <returns> Объект с данными о комплексе и его продавце.</returns>
		public static Complex Create(ComplexHelper complex, string conditionsOfPaymentFees, bool isSellerCommissionPrepayments)
		{
			var houseGroupsCurrents = complex.HousesInComplex.Select(HouseGroupFactory.Create).ToArray();
			var commission = CreateCommissionRangeValueInComplex(houseGroupsCurrents);
			var housesGrouping =
				HouseGroupFactory.GroupingHouses(houseGroupsCurrents.OrderBy(x => x.HouseName).ToArray());
			var houseCount = commission.MaxCommissionValue > commission.MinCommissionValue
				? housesGrouping.Count
				: housesGrouping.Count(x => x.ObjectGroups.Count > 0 || x.CommissionType != commission.CommissionType);

			var model = new Complex
			{
				ComplexId = complex.ComplexId,
				ComplexName = complex.ComplexName,
				IsAdvancedBooking = complex.IsAdvancedBooking,
				IsCommissionCalculatedFromTotalPrice = complex.IsCommissionCalculatedFromTotalPrice,
				RealtyObjectType = complex.RealtyObjectType,
				SellerId = complex.SellerId,
				SellerType = complex.SellerType,
				RegionGroupId = complex.RegionGroupId,
				SellerName = complex.SellerName,
				HouseGroups = housesGrouping,
				CommissionType = commission.CommissionType,
				MaxCommissionValue = commission.MaxCommissionValue,
				MinCommissionValue = commission.MinCommissionValue,
				ConditionsOfPaymentFees = conditionsOfPaymentFees,
				IsSellerCommissionPrepayments = isSellerCommissionPrepayments,
				HousesCount = houseCount,
				CrossRegionAdvancedBookingCoefficient = complex.CrossRegionAdvancedBookingCoefficient,
				UrlLandingPrepaymentBooking = complex.UrlLandingPrepaymentBooking,
			};

			return model;
		}

		/// <summary>
		/// Определение минимального и максимального значения и типа комиссии субагента в комплексе.
		/// </summary>
		/// <param name="houseGroupsCurrents"> Данные о корпусах в комплексе.</param>
		/// <returns> Минимальное и максимальное значения и тип комиссии субагента в комплексе.</returns>
		private static CommissionRangeHelpers CreateCommissionRangeValueInComplex(IReadOnlyCollection<HouseGroup> houseGroupsCurrents)
		{
			if (!houseGroupsCurrents.All(c => c.CommissionType is CommissionType.Percent or null) &&
			    !houseGroupsCurrents.All(c => c.CommissionType is CommissionType.AbsolutePerSqMeter or null) &&
			    !houseGroupsCurrents.All(c => c.CommissionType is CommissionType.Absolute or null))
			{
				return new CommissionRangeHelpers
				{
					CommissionType = null,
					MaxCommissionValue = null,
					MinCommissionValue = null,
				};
			}

			decimal? minCommissionValue = null;
			decimal? maxCommissionValue = null;
			CommissionType? commissionType = null;

			CommissionRangeHelpers individualCommissionIsSet = GetCommissionRangeIsSetCustomCommission(houseGroupsCurrents);
			CommissionRangeHelpers individualCommissionIsNotSet = GetCommissionRangeIsNotSetCustomCommission(houseGroupsCurrents);

			if (individualCommissionIsNotSet.MinCommissionValue != null)
			{
				minCommissionValue = new[]
				{
					individualCommissionIsSet.MinCommissionValue,
					individualCommissionIsNotSet.MinCommissionValue,
				}.Min();
			}
			else
			{
				minCommissionValue = individualCommissionIsSet.MinCommissionValue;
			}

			if (individualCommissionIsNotSet.MaxCommissionValue != null)
			{
				maxCommissionValue = new[]
				{
					individualCommissionIsSet.MaxCommissionValue,
					individualCommissionIsNotSet.MaxCommissionValue,
				}.Max();
			}
			else
			{
				maxCommissionValue = individualCommissionIsSet.MaxCommissionValue;
			}

			commissionType = individualCommissionIsSet.CommissionType ?? individualCommissionIsNotSet.CommissionType;

			return new CommissionRangeHelpers
			{
				CommissionType = commissionType,
				MaxCommissionValue = maxCommissionValue,
				MinCommissionValue = minCommissionValue,
			};
		}

		/// <summary>
		/// Вычисляем мин/макс. комиссии, если у нас есть индивидуальные комиссии на корпус.
		/// </summary>
		/// <param name="houseGroupsCurrents">Данные о корпусах в комплексе.</param>
		/// <returns>Минимальное и максимальное значения и тип комиссии субагента в комплексе.</returns>
		private static CommissionRangeHelpers GetCommissionRangeIsSetCustomCommission(IReadOnlyCollection<HouseGroup> houseGroupsCurrents)
		{
			CommissionRangeHelpers commissionRange = new CommissionRangeHelpers();

			var commissionTypeOfHouses = houseGroupsCurrents
				.Where(x => x.CommissionType != null)
				.GroupBy(x => x.CommissionType)
				.Select(x => new
				{
					CommissionType = x.Key,
					MinCommssionValue = x.Min(i => i.CommissionValue),
					MaxCommssionValue = x.Max(i => i.CommissionValue),
					Objects = x.SelectMany(i => i.ObjectGroups).ToArray(),
				})
				.FirstOrDefault();

			if (commissionTypeOfHouses != null)
			{
				commissionRange.CommissionType = commissionTypeOfHouses.CommissionType;

				var objectsCommissionMinMaxValues = commissionTypeOfHouses.Objects
					.Where(x => x.CommissionType == commissionTypeOfHouses.CommissionType)
					.GroupBy(x => x.CommissionType)
					.Select(x => new
					{
						MinCommissionValue = x.Min(i => i.CommissionValue),
						MaxCommssionValue = x.Max(i => i.CommissionValue),
					})
					.FirstOrDefault();

				if (objectsCommissionMinMaxValues != null)
				{
					commissionRange.MinCommissionValue = new[]
					{
						commissionTypeOfHouses.MinCommssionValue,
						objectsCommissionMinMaxValues.MinCommissionValue,
					}.Min();

					commissionRange.MaxCommissionValue = new[]
					{
						commissionTypeOfHouses.MaxCommssionValue,
						objectsCommissionMinMaxValues.MaxCommssionValue,
					}.Max();
				}
				else
				{
					commissionRange.MinCommissionValue = commissionTypeOfHouses.MinCommssionValue;
					commissionRange.MaxCommissionValue = commissionTypeOfHouses.MaxCommssionValue;
				}
			}

			return commissionRange;
		}

		/// <summary>
		/// Вычисляем мин/макс. комиссии, если у нас НЕТ индивидуальных комиссий на корпус.
		/// </summary>
		/// <param name="houseGroupsCurrents">Данные о корпусах в комплексе.</param>
		/// <returns>Минимальное и максимальное значения и тип комиссии субагента в комплексе.</returns>
		private static CommissionRangeHelpers GetCommissionRangeIsNotSetCustomCommission(IReadOnlyCollection<HouseGroup> houseGroupsCurrents)
		{
			CommissionRangeHelpers commissionRange = new CommissionRangeHelpers();

			var commissionTypeOfObjects = houseGroupsCurrents
				.Where(x => x.CommissionType == null && x.CommissionValue == null)
				.GroupBy(x => x.MinMaxCommissionType)
				.OrderByDescending(x => x.Key == CommissionType.Percent)
				.ThenByDescending(x => x.Key == CommissionType.AbsolutePerSqMeter)
				.ThenByDescending(x => x.Key == CommissionType.Absolute)
				.Select(x => new
				{
					CommissionType = x.Key,
					MinCommssionValue = x.Min(i => i.MinCommissionValue),
					MaxCommssionValue = x.Max(i => i.MaxCommissionValue),
				})
				.FirstOrDefault();

			if (commissionTypeOfObjects != null)
			{
				commissionRange.CommissionType = commissionTypeOfObjects.CommissionType;
				commissionRange.MinCommissionValue = commissionTypeOfObjects.MinCommssionValue;
				commissionRange.MaxCommissionValue = commissionTypeOfObjects.MaxCommssionValue;
			}

			return commissionRange;
		}
	}
}