using System.Collections.Generic;

using TariffCardService.Core.Enum;

namespace TariffCardService.Core.Dto
{
    /// <summary>
    /// Данные о группе домов в комплексе для страницы комиссий.
    /// </summary>
    public class HouseGroupDto
    {
        /// <summary>
        /// ID записи.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// ID дома.
        /// </summary>
        public long? HouseId { get; set; }

        /// <summary>
        /// Наименование дома.
        /// </summary>
        public string HouseName { get; set; }

        /// <summary>
        /// Тип комиссий для точного значения.
        /// </summary>
        public CommissionType? CommissionType { get; set; }

        /// <summary>
        /// Точное значение комиссии.
        /// </summary>
        public decimal? CommissionValue { get; set; }

        /// <summary>
        /// Тип комиссий для интервала.
        /// </summary>
        public CommissionType? MinMaxCommissionType { get; set; }

        /// <summary>
        /// Минимальное значение комиссий.
        /// </summary>
        public decimal? MinCommissionValue { get; set; }

        /// <summary>
        /// Максимальное значение комиссий.
        /// </summary>
        public decimal? MaxCommissionValue { get; set; }

        /// <summary>
        /// Значение кросс-регионального коэффициента расширенного бронирования
        /// для региона местонахождения корпуса.
        /// </summary>
        public decimal? CrossRegionAdvancedBookingCoefficient { get; set; }

        /// <summary>
        /// Тип объекта недвижимости.
        /// </summary>
        public RealtyObjectType? RealtyObjectType { get; set; }

        /// <summary>
        /// Признак наличия объектов с переопределенными комисионными в корпусе..
        /// </summary>
        public bool HasOverriding { get; set; }

        /// <summary>
        /// Группы объектов.
        /// </summary>
        public ICollection<ObjectGroupDto> ObjectGroups { get; set; }
    }
}
