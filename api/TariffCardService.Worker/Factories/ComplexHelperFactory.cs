using System.Collections.Generic;

using TariffCardService.Worker.Helpers;

namespace TariffCardService.Worker.Factories
{
    /// <summary>
    /// Методы создания вспомогательных объектов с данными о комлексах и их продавцах.
    /// </summary>
	public static class ComplexHelperFactory
    {
	    /// <summary>
	    /// Создание вспомогательного объекта с данными о комплексах и продавцах.
	    /// </summary>
	    /// <param name="house"> Данные о корпусе. </param>
	    /// <param name="urlLandingPrepaymentBooking"> Ссылка на информационный ресурс - лендинг авансирования.</param>
	    /// <param name="regionGroupId">Региональная группа объекта.</param>
	    /// <returns> Вспомогательный объект с данными о комплексах и продавцах.</returns>
	    public static ComplexHelper Create(HouseHelper house, string urlLandingPrepaymentBooking, int regionGroupId) =>
		    new ()
		    {
			    ComplexId = house.ComplexId,
			    ComplexName = house.ComplexName,
			    IsAdvancedBooking = house.IsAdvancedBooking,
			    IsCommissionCalculatedFromTotalPrice = house.IsCommissionCalculatedFromTotalPrice,
			    RealtyObjectType = house.RealtyObjectType,
			    SellerId = house.SellerId,
			    SellerType = house.SellerType,
			    RegionGroupId = regionGroupId,
			    SellerName = house.SellerName,
			    IsComplex = house.IsComplex,
			    CrossRegionAdvancedBookingCoefficient = house.CrossRegionAdvancedBookingCoefficient,
			    UrlLandingPrepaymentBooking = urlLandingPrepaymentBooking,
			    HousesInComplex = new List<HouseHelper> { house },
		    };
    }
}