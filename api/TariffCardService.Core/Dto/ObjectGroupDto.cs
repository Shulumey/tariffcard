using TariffCardService.Core.Enum;

namespace TariffCardService.Core.Dto
{
    /// <summary>
    /// Данные о группе объектов в доме для страницы комиссий.
    /// </summary>
    public class ObjectGroupDto
    {
        /// <summary>
        /// ID записи.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Количество комнат.
        /// </summary>
        public int? Rooms { get; set; }

        /// <summary>
        /// ID квартиры.
        /// </summary>
        public long? ApartmentId { get; set; }

        /// <summary>
        /// Описание квартиры.
        /// </summary>
        public string ApartmentDescription { get; set; }

        /// <summary>
        /// Тип комиссий.
        /// </summary>
        public CommissionType? CommissionType { get; set; }

        /// <summary>
        /// Значение комиссий.
        /// </summary>
        public decimal? CommissionValue { get; set; }

        /// <summary>
        /// Значение кросс-регионального коэффициента расширенного бронирования
        /// для региона местонахождения объекта.
        /// </summary>
        public decimal? CrossRegionAdvancedBookingCoefficient { get; set; }

        /// <summary>
        /// Признак, что комиссионные на объект были переопределены.
        /// </summary>
        public bool IsOverriding { get; set; }
    }
}
