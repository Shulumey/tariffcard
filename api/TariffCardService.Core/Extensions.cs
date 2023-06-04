namespace TariffCardService.Core
{
	/// <summary>
	/// Стандартные расширения сервиса.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Сравнивает Nullable объекты и заменяет значение целевого если они не совпадают.
		/// </summary>
		/// <param name="target"> Целевой объект.</param>
		/// /// <param name="comparable"> Сравниваемый.</param>
		/// <typeparam name="T"> Тип сравниваемых объектов. Nullable тип.</typeparam>
		/// <returns>Возвращает обновленный целевой объект.</returns>
		public static T? ObjectEqualsAndUpdateNullableType<T>(this T? target, T? comparable)
			where T : struct
			=> !target.Equals(comparable) ? comparable : target;

		/// <summary>
		/// Сравнивает не Nullable объекты и заменяет значение целевого, если они не совпадают.
		/// </summary>
		/// <param name="target"> Целевой объект.</param>
		/// <param name="comparable"> Сравниваемый.</param>
		/// <typeparam name="T"> Тип сравниваемых объектов.</typeparam>
		/// <returns>Возвращает обновленный целевой объект.</returns>
		public static T ObjectEqualsAndUpdate<T>(this T target, T comparable) => !target.Equals(comparable) ? comparable : target;

		/// <summary>
		/// Сравнивает строки и заменяет значение целевой, если они не совпадают.
		/// </summary>
		/// <param name="target"> Целевая строка.</param>
		/// <param name="comparable"> Сравниваемая.</param>
		/// <returns> Возвращает обновленную целевую строку.</returns>
		public static string ObjectEqualsAndUpdateString(this string target, string comparable) => target == comparable ? target : comparable;
	}
}
