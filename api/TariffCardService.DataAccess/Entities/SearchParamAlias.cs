using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace TariffCardService.DataAccess.Entities
{
	/// <summary>
	///  Класс сопоставления синонима и строки поиска.
	/// </summary>
	[Table("SearchParamsAliases")]
	[Index(nameof(Alias), nameof(Value))]
	public class SearchParamAlias
	{
		/// <summary>
		/// Идентификатор синонима.
		/// </summary>
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		/// <summary>
		/// Псевдоним.
		/// </summary>
		[Column("Alias")]
		public string Alias { get; set; }

		/// <summary>
		///  Действительное значение.
		/// </summary>
		[Column("DisplayName")]
		public string Value { get; set; }

		/// <summary>
		/// ID региона.
		/// </summary>
		[Column("RegionGroupId")]
		public int RegionalGroupId { get; set; }
	}
}