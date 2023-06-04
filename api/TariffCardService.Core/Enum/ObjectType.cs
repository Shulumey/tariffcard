namespace TariffCardService.Core.Enum
{
	/// <summary>
	/// Тип объекта недвижимости.
	/// </summary>
	public enum ObjectType
	{
		/// <summary>
		/// Квартиры новостроя.
		/// </summary>
		NbApartment = 3,

		/// <summary>
		/// Дома новостроя.
		/// </summary>
		NbHouse = 7,

		/// <summary>
		/// Фото пользователя.
		/// </summary>
		PlayerPicture = 12,

		/// <summary>
		/// Застройщик.
		/// </summary>
		Developer = 3002,

		/// <summary>
		/// Клиент брокера.
		/// </summary>
		BrokerClient = 3030,

		/// <summary>
		/// Компания брокера.
		/// </summary>
		BrokerCompany = 3040,

		/// <summary>
		/// Бронь.
		/// </summary>
		Booking = 3050,

		/// <summary>
		/// Отдел оператора.
		/// </summary>
		OperatorDepartment = 3070,

		/// <summary>
		/// Элемент инфоблока.
		/// </summary>
		InfoBlockElement = 4000,

		/// <summary>
		/// Екаталог.
		/// </summary>
		ECatalog = 11000,
	}
}