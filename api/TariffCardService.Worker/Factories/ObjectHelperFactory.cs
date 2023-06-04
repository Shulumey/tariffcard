using System.Collections.Generic;

using TariffCardService.Core.Enum;

using TariffCardService.Worker.Entities;
using TariffCardService.Worker.Helpers;

namespace TariffCardService.Worker.Factories
{
	/// <summary>
	/// Методы создания и обработки объектов с данными опормещениях.
	/// </summary>
	public static class ObjectHelperFactory
	{
		/// <summary>
		/// Cловарь данных для формирования описания помещения.
		/// </summary>
		internal static readonly Dictionary<RealtyObjectType, string> ApartmentDescriptionDictionary = new ()
		{
			{ RealtyObjectType.Apartment, "К" },
			{ RealtyObjectType.CommercialApartment, "А" },
			{ RealtyObjectType.Commercial, "КО" },
			{ RealtyObjectType.Parking, "ММ" },
			{ RealtyObjectType.Storeroom, "КЛ" },
		};

		/// <summary>
		/// Cловарь данных для формирования описания студий.
		/// </summary>
		internal static readonly Dictionary<RealtyObjectType, string> StudioDescriptionDictionary = new ()
		{
			{ RealtyObjectType.Apartment, "СТ" },
			{ RealtyObjectType.CommercialApartment, "СА" },
		};

		/// <summary>
		/// Метод создания объекта с данными о помещениях.
		/// </summary>
		/// <param name="apartment"> Данные о помещении.</param>
		/// <param name="tariffsCrossRegionCoefficient"> Коэффициент кросс-регионального расширенного бронирования для данного объекта.</param>
		/// <returns>Объект класса ObjectHelpers с данными о помещении.</returns>
		public static ObjectHelper Create(NMarketApartmentLocalEntity apartment, decimal? tariffsCrossRegionCoefficient) =>
			new ()
			{
				Rooms = apartment.RealtyObjectType == RealtyObjectType.Commercial
					? 1 // комнатность у помещений коммерции всегда выставляется в 1 для удобства группировки объектов.
					: apartment.Rooms,
				RealtyObjectType = apartment.RealtyObjectType,
				CommissionType = apartment.ViewNMarketApartmentCommissions.TotalCommissionType,
				CommissionValue = apartment.ViewNMarketApartmentCommissions.TotalCommissionValue,
				ApartmentDescription = GetApartmentDescription(apartment),
				ApartmentId = apartment.ViewNMarketApartmentCommissions.ApartmentLevelCommissionValue.HasValue
					? apartment.ApartmentId
					: null,
				CrossRegionAdvancedBookingCoefficient = tariffsCrossRegionCoefficient ?? 1,
				IsOverriding = GetOverridingApartment(apartment),
				SellerId = apartment.ViewNMarketApartmentComparisonSellers.PublicSellerPropertiesId,
				SellerType = apartment.SellerType,
				HouseId = apartment.HouseId,
			};

		/// <summary>
		/// Получение описания помещения.
		/// </summary>
		/// <param name="apartment"> Данные о помещении.</param>
		/// <returns> Описание помещения.</returns>
		private static string GetApartmentDescription(NMarketApartmentLocalEntity apartment)
		{
			var partApartmentDescription = ApartmentDescriptionDictionary[apartment.RealtyObjectType];
			var isApartmentOrCommercialApartment =
				apartment.RealtyObjectType is RealtyObjectType.Apartment or RealtyObjectType.CommercialApartment;

			return apartment.ViewNMarketApartmentCommissions.ApartmentLevelCommissionValue.HasValue
				? isApartmentOrCommercialApartment
					? string.Join(" ", "№", apartment.ApartmentNumber, apartment.IsStudio ? StudioDescriptionDictionary[apartment.RealtyObjectType] : apartment.Rooms + partApartmentDescription, apartment.SquareTotal, "м&#178;")
					: string.Join(" ", "№", string.IsNullOrEmpty(apartment.ApartmentNumber) ? apartment.Type : apartment.ApartmentNumber, partApartmentDescription, apartment.SquareTotal, "м&#178;")
				: isApartmentOrCommercialApartment
					? apartment.IsStudio ? StudioDescriptionDictionary[apartment.RealtyObjectType]
					: apartment.Rooms + partApartmentDescription
					: partApartmentDescription;
		}

		/// <summary>
		/// Получение признака переопределения комиссионных помещения.
		/// </summary>
		/// <param name="apartment"> Данные о помещении.</param>
		/// <returns> Признак переопределение комиссионных помещения.</returns>
		private static bool GetOverridingApartment(NMarketApartmentLocalEntity apartment) =>
			apartment.ViewNMarketApartmentCommissions.ApartmentLevelCommissionValue.HasValue ||
			apartment.ViewNMarketApartmentCommissions.HouseLevelCommissionValue.HasValue;
	}
}