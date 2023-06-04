namespace TariffCardService.Core.Enum
{
	/// <summary>
	/// Типы уровней комиссионных.
	/// </summary>
	public enum CommissionLevelType
	{
		/// <summary>
		/// Застройщик
		/// </summary>
		Builder = 1,

		/// <summary>
		/// Комнатность для застройщика
		/// </summary>
		RoomsBuilder = 2,

		/// <summary>
		/// Дом
		/// </summary>
		House = 3,

		/// <summary>
		/// Комнатность для дома
		/// </summary>
		RoomsHouse = 4,

		/// <summary>
		/// Объект
		/// </summary>
		ObjectHouse = 5,
	}
}