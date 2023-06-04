using System.Collections.Generic;

using TariffCardService.Core.Enum;

namespace TariffCardService.Worker.Helpers
{
    /// <summary>
    /// Класс для временного хранения данных об комплексе и продавце.
    /// </summary>
    public class ComplexHelper
    {
	    /// <summary>
        /// Идентификатор региональной группы местонахождения комплекса.
        /// </summary>
        public int RegionGroupId { get; set; }

        /// <summary>
        /// Идентификатор компании в базе НМаркет.
        /// </summary>
        public int? SellerId { get; set; }

        /// <summary>
        /// Имя компании.
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// Тип продавца.
        /// </summary>
        public SellerType SellerType { get; set; }

        /// <summary>
        /// Идентификатор комплекса в базе НМаркет.
        /// </summary>
        public long? ComplexId { get; set; }

        /// <summary>
        /// Название комплекса.
        /// </summary>
        public string ComplexName { get; set; }

        /// <summary>
        /// Признак, что работа с этой компанией предоставляется только в рамках услуги "Расширенное бронирование".
        /// </summary>
        public bool IsAdvancedBooking { get; set; }

        /// <summary>
        /// Признак, что комиссия считается от цены при полной оплате.
        /// </summary>
        public bool IsCommissionCalculatedFromTotalPrice { get; set; }

        /// <summary>
        /// Тип объектов в комплексе.
        /// </summary>
        public RealtyObjectType RealtyObjectType { get; set; }

        /// <summary>
        /// Признак, что дом/корпус является комплексом.
        /// </summary>
        public bool IsComplex { get; set; }

        /// <summary>
        /// Значение кросс-регионального коэффициента расширенного бронирования.
        /// </summary>
        public decimal CrossRegionAdvancedBookingCoefficient { get; set; }

        /// <summary>
        /// Url-адрес лендинга авансирования для региона местонахождения комплекса.
        /// </summary>
        public string UrlLandingPrepaymentBooking { get; set; }

        /// <summary>
        /// Корпуса в комплексе.
        /// </summary>
        public ICollection<HouseHelper> HousesInComplex { get; set; }
    }
}