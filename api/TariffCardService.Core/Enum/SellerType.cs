namespace TariffCardService.Core.Enum
{
    /// <summary>
    /// Тип продавца.
    /// </summary>
    public enum SellerType
    {
        /// <summary>
        /// Застройщик
        /// </summary>
        Developer = 1,

        /// <summary>
        /// Подрядчик
        /// </summary>
        Contractor = 2,

        /// <summary>
        /// Переуступка для юр. лиц
        /// </summary>
        AssignmentLegalEntity = 3,
    }
}