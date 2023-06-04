using System.Collections.Generic;

using TariffCardService.Core.Enum;

namespace TariffCardService.Worker.Helpers
{
    /// <summary>
    /// Класс для временного хранения данных о корпусе.
    /// </summary>
    public class HouseHelper
    {
	    /// <summary>
        /// Идентификатор корпуса в базе НМаркет.
        /// </summary>
        public long? HouseId { get; set; }

        /// <summary>
        /// Идентификатор комплекса в базе НМаркет.
        /// </summary>
        public long? ComplexId { get; set; }

        /// <summary>
        /// Признак, что дом/корпус является комплексом.
        /// </summary>
        public bool IsComplex { get; set; }

        /// <summary>
        /// Название комплекса.
        /// </summary>
        public string ComplexName { get; set; }

        /// <summary>
        /// Название корпуса.
        /// </summary>
        public string HouseName { get; set; }

        /// <summary>
        /// Тип комиссии на корпус.
        /// </summary>
        public CommissionType? CommissionType { get; set; }

        /// <summary>
        /// Значение комиссии на корпус.
        /// </summary>
        public decimal? CommissionValue { get; set; }

        /// <summary>
        /// Тип общей комиссии застройщика.
        /// </summary>
        public CommissionType? SellerCommissionType { get; set; }

        /// <summary>
        /// Значение общей комиссии застройщика.
        /// </summary>
        public decimal? SellerCommissionValue { get; set; }

        /// <summary>
        /// Тип объектов в корпусе.
        /// </summary>
        public RealtyObjectType RealtyObjectType { get; set; }

        /// <summary>
        /// Идентификатор компании-продавца.
        /// </summary>
        public int? SellerId { get; set; }

        /// <summary>
        /// Наименование компании-продавца.
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// Тип компании-продавца.
        /// </summary>
        public SellerType SellerType { get; set; }

        /// <summary>
        /// Значение кросс-регионального коэффициента расширенного бронирования.
        /// </summary>
        public decimal CrossRegionAdvancedBookingCoefficient { get; set; }

        /// <summary>
        /// Идентификатор региона местонахождения комплекса.
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// Признак, что работа с этой компанией предоставляется только в рамках услуги "Расширенное бронирование".
        /// </summary>
        public bool IsAdvancedBooking { get; set; }

        /// <summary>
        /// Признак, что комиссия считается от цены при полной оплате.
        /// </summary>
        public bool IsCommissionCalculatedFromTotalPrice { get; set; }

        /// <summary>
        /// Помещения в корпусе, находящиеся в статусе Активно и Зарезервировано.
        /// </summary>
        public List<ObjectHelper> ObjectsInHouse { get; set; }
    }
}