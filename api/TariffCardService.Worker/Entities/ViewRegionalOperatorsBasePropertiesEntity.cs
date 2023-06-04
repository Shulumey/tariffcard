using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Сопоставление регионального оператора и его региональной группы(из View).
	/// </summary>
	[Table("viewRegionalOperatorsBase", Schema = "brokerage")]
	public class ViewRegionalOperatorsBasePropertiesEntity
	{
		/// <summary>
		/// Id регионального оператора.
		/// </summary>
		[Key]
		[Column("Id")]
		public int? Id { get; set; }

		/// <summary>
		/// Id региональной группы.
		/// </summary>
		[Column("RegionGroupId")]
		public int? RegionGroupId { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="ViewRegionalOperatorsBasePropertiesEntity"/>.
		/// </summary>
		public class
			ViewRegionalOperatorsBasePropertiesConfiguration : IEntityTypeConfiguration<ViewRegionalOperatorsBasePropertiesEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="ViewRegionalOperatorsBasePropertiesEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<ViewRegionalOperatorsBasePropertiesEntity> builder)
			{
			}
		}
	}
}