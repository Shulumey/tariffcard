using System.Collections.Generic;

using TariffCardService.Core.Enum;
using TariffCardService.Worker.Entities;
using TariffCardService.Worker.Helpers;

namespace TariffCardService.Worker.Factories
{
    /// <summary>
    /// Методы создания вспомогательных объектов с данными о корпусах.
    /// </summary>
	public static class HouseHelperFactory
    {
	    /// <summary>
	    /// Создание вспомогательного объекта с данными о корпусах.
	    /// </summary>
	    /// <param name="apartment">Данные о помещении.</param>
	    /// <param name="house"> Данные о корпусе и его продавце.</param>
	    /// <param name="tariffsCrossRegionCoefficient"> Коэффициент кросс-регионального расширенного бронирования для данного объекта.</param>
	    /// <returns> Вспомогательный объект с данными о корпусах.</returns>
	    public static HouseHelper Create(NMarketApartmentLocalEntity apartment, NMarketHouseLocalEntity house, decimal? tariffsCrossRegionCoefficient)
	    {
		    var builder = apartment.ViewNMarketApartmentComparisonSellers.SellerProperties;
		    var model = new HouseHelper
            {
                HouseId = apartment.HouseId,
                ComplexId = house.IsComplex ? house.HouseId : house.ComplexId,
                IsComplex = house.IsComplex,
                ComplexName = house.HouseName,
                HouseName = string.Join(" ", house.StageNumber.ToString(), "оч.", house.BuildingNumber, "корп."),
                RealtyObjectType = apartment.RealtyObjectType == RealtyObjectType.CommercialApartment ? RealtyObjectType.Apartment : apartment.RealtyObjectType,
                CommissionType = apartment.ViewNMarketApartmentCommissions.HouseLevelCommissionType,
                CommissionValue = apartment.ViewNMarketApartmentCommissions.HouseLevelCommissionValue,
                SellerCommissionType = apartment.ViewNMarketApartmentCommissions.SellerLevelCommissionType,
                SellerCommissionValue = apartment.ViewNMarketApartmentCommissions.SellerLevelCommissionValue,
                SellerId = builder.BuilderId,
                SellerName = builder.Name,
                SellerType = apartment.SellerType,
                ObjectsInHouse = new List<ObjectHelper>(),
                CrossRegionAdvancedBookingCoefficient = tariffsCrossRegionCoefficient ?? 1,
                RegionId = apartment.RegionId,
                IsAdvancedBooking = builder.IsAdvancedBooking,
                IsCommissionCalculatedFromTotalPrice = builder.IsCommissionCalculatedFromTotalPrice,
            };

		    return model;
        }
    }
}