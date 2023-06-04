using System.Collections.Generic;
using System.Linq;

using TariffCardService.Core.Models;

using TariffCardService.Worker.Helpers;

namespace TariffCardService.Worker.Factories
{
	/// <summary>
	/// Методы создания и обработки объектов с данными опормещениях.
	/// </summary>
	public static class ObjectGroupFactory
	{
		/// <summary>
		/// Метод создания объекта с с данными о помещениях.
		/// </summary>
		/// <param name="apartment"> Данные о помещении.</param>
		/// <returns>Объект класса ObjectGroup с данными о помещении.</returns>
		public static ObjectGroup Create(ObjectHelper apartment) =>
			new ()
			{
				Rooms = apartment.Rooms,
				RealtyObjectType = apartment.RealtyObjectType,
				CommissionType = apartment.CommissionType,
				CommissionValue = apartment.CommissionValue,
				ApartmentDescription = apartment.ApartmentDescription,
				ApartmentId = apartment.ApartmentId,
				CrossRegionAdvancedBookingCoefficient = apartment.CrossRegionAdvancedBookingCoefficient,
				IsOverriding = apartment.IsOverriding,
			};

		/// <summary>
		/// Группировка помещений по общим параметрам при отсутствии идентификатора помещения и совпадении всех его атрибутов.
		/// </summary>
		/// <param name="apartments"> Данные о помещениях для группировки.</param>
		/// <returns> Коллекция сгруппированных помещений.</returns>
		public static ICollection<ObjectGroup> GroupingObject(ICollection<ObjectGroup> apartments)
		{
			var apartmentsGrouping = apartments.Where(x => x.ApartmentId.HasValue).ToList();
			foreach (var apartment in apartments.Where(x => !x.ApartmentId.HasValue))
			{
				if (apartmentsGrouping.All(x => x.CommissionType != apartment.CommissionType ||
				                                x.CommissionValue != apartment.CommissionValue ||
				                                x.RealtyObjectType != apartment.RealtyObjectType ||
				                                x.ApartmentId != apartment.ApartmentId ||
				                                x.IsOverriding != apartment.IsOverriding))
				{
					var similarApartments = apartments.Where(x => x.CommissionType == apartment.CommissionType &&
					                                              x.CommissionValue == apartment.CommissionValue &&
					                                              x.RealtyObjectType == apartment.RealtyObjectType &&
					                                              x.ApartmentId == apartment.ApartmentId &&
					                                              x.IsOverriding == apartment.IsOverriding)
						.Select(x => x.ApartmentDescription).ToArray();

					apartmentsGrouping.Add(new ObjectGroup
					{
						Rooms = null,
						RealtyObjectType = apartment.RealtyObjectType,
						CommissionType = apartment.CommissionType,
						CommissionValue = apartment.CommissionValue,
						ApartmentDescription = string.Join(", ", similarApartments),
						ApartmentId = null,
						IsOverriding = apartment.IsOverriding,
						CrossRegionAdvancedBookingCoefficient = apartment.CrossRegionAdvancedBookingCoefficient,
					});
				}
			}

			return apartmentsGrouping;
		}
	}
}