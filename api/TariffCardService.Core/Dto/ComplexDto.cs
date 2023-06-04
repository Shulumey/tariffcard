using TariffCardService.Core.Enum;

namespace TariffCardService.Core.Dto
{
    /// <summary>
    /// Данные о комплексе для страницы комиссий.
    /// </summary>
    public class ComplexDto
    {
        /// <summary>
        /// ID записи.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// ID продавца.
        /// </summary>
        public int? SellerId { get; set; }

        /// <summary>
        /// Наименование продавца.
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// Наименование комплекса.
        /// </summary>
        public string ComplexName { get; set; }

        /// <summary>
        /// Тип комиссий.
        /// </summary>
        public CommissionType? CommissionType { get; set; }

        /// <summary>
        /// Минимальное значение комиссий.
        /// </summary>
        public decimal? MinCommissionValue { get; set; }

        /// <summary>
        /// Максимальное значение комиссий.
        /// </summary>
        public decimal? MaxCommissionValue { get; set; }

        /// <summary>
        /// Признак, что работа с этой компанией предоставляется в рамках услуги "Расширенное бронирование".
        /// </summary>
        public bool? IsAdvancedBooking { get; set; }

        /// <summary>
        /// Признак, что комиссия считается от цены при полной оплате.
        /// </summary>
        public bool? IsCommissionCalculatedFromTotalPrice { get; set; }

        /// <summary>
        /// Признак возможности использования расширенного авансирования для компании-продавца.
        /// </summary>
        public bool IsSellerCommissionPrepayments { get; set; }

        /// <summary>
        /// Условия выплаты.
        /// </summary>
        public string ConditionsOfPaymentFees { get; set; }

        /// <summary>
        /// Значение кросс-регионального коэффициента расширенного бронирования
        /// для региона местонахождения комплекса.
        /// </summary>
        public decimal? CrossRegionAdvancedBookingCoefficient { get; set; }

        /// <summary>
        /// Url-адрес лендинга авансирования для региона местонахождения комплекса.
        /// </summary>
        public string UrlLandingPrepaymentBooking { get; set; }

        /// <summary>
        /// Количество домов в комплексе.
        /// </summary>
        public int HousesCount { get; set; }

        /// <summary>
        /// Тип объекта недвижимости.
        /// </summary>
        public RealtyObjectType RealtyObjectType { get; set; }
    }
}
