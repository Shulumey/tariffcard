using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	///  Псевдонимы для поиска.
	/// </summary>
	[Table("SearchParameterAlias", Schema = "dbo")]
	public class SearchParameterAliasEntity
	{
		/// <summary>
		/// Идентификатор записи.
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
		///  Действительное значение для поиска.
		/// </summary>
		[Column("DisplayValue")]
		public string ActualValue { get; set; }

		/// <summary>
		/// ID региона.
		/// </summary>
		[Column("RegionGroupId")]
		public int? RegionalGroupId { get; set; }
	}
}