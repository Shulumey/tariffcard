namespace TariffCardService.Core.Enum
{
	/// <summary>
	/// Тип комиссионных.
	/// </summary>
	public enum CommissionType
	{
		/// <summary>
		/// Абсолютная сумма
		/// </summary>
		Absolute = 1,

		/// <summary>
		/// Процент от общей цены
		/// </summary>
		Percent = 2,

		/// <summary>
		/// Абсолютная сумма за кв. м.
		/// </summary>
		AbsolutePerSqMeter = 3,
	}
}