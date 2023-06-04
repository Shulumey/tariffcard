namespace TariffCardService.Core.Models
{
	/// <summary>
	///  Класс сопоставления синонима и строки поиска.
	/// </summary>
	public class SearchParamAlias
	{
		/// <summary>
		/// Идентификатор синонима.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Псевдоним.
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		///  Действительное значение.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// ID региона.
		/// </summary>
		public int RegionalGroupId { get; set; }
	}
}