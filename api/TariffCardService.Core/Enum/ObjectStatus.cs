namespace TariffCardService.Core.Enum
{
	/// <summary>
	/// Статус объекта продажи.
	/// </summary>
	public enum ObjectStatus
	{
		/// <summary>
		/// Вводятся данные
		/// </summary>
		Enter = 1,

		/// <summary>
		/// Активно
		/// </summary>
		Active = 2,

		/// <summary>
		/// Архивно
		/// </summary>
		Archive = 3,

		/// <summary>
		/// Сдано
		/// </summary>
		Rented = 4,

		/// <summary>
		/// Ведется договор
		/// </summary>
		ContractIsBeing = 5,

		/// <summary>
		/// Продано
		/// </summary>
		SoldOut = 6,

		/// <summary>
		/// Зарезервировано
		/// </summary>
		Reserved = 7,

		/// <summary>
		/// Предварительное бронирование
		/// </summary>
		PreBooking = 8,

		/// <summary>
		/// Скоро в продаже
		/// </summary>
		ComingSoon = 10,
	}
}