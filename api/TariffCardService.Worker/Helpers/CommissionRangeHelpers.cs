using TariffCardService.Core.Enum;

namespace TariffCardService.Worker.Helpers
{
    /// <summary>
    /// Вспомогательный объект хранения данных о минимальном и максимальном значении и типе комиссии субагента.
    /// </summary>
    public class CommissionRangeHelpers
    {
        /// <summary>
        /// Тип комиссионных.
        /// </summary>
        public CommissionType? CommissionType { get; set; }

        /// <summary>
        /// Минимальное значение комиссии.
        /// </summary>
        public decimal? MinCommissionValue { get; set; }

        /// <summary>
        /// Максимальное значение комиссии.
        /// </summary>
        public decimal? MaxCommissionValue { get; set; }
    }
}