using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Данные со ссылками, на информационные ресурсы региональных операторов.
	/// </summary>
	[Table("RegionOperatorReference", Schema = "dbo")]
	public class RegionOperatorReferenceEntity
	{
		/// <summary>
		/// Идентификатор записи.
		/// </summary>
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		/// <summary>
		/// Идентификатор регионального оператора.
		/// </summary>
		[ForeignKey("RegionalOperatorsBaseProperties")]
		[Column("RegionOperatorId")]
		public int RegionalOperatorsBasePropertiesId { get; set; }

		/// <summary>
		/// URL информационного ресурса регионального оператора.
		/// </summary>
		[Column("Url")]
		public string Url { get; set; }

		/// <summary>
		/// Тип ресурса.
		/// </summary>
		[Column("TRegionOperatorReferenceId")]
		public int RegionOperatorReferenceType { get; set; }

		/// <inheritdoc cref="ViewRegionalOperatorsBasePropertiesEntity"/>
		public ViewRegionalOperatorsBasePropertiesEntity RegionalOperatorsBaseProperties { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="RegionOperatorReferenceEntity"/>.
		/// </summary>
		public class RegionOperatorReferenceConfiguration : IEntityTypeConfiguration<RegionOperatorReferenceEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="RegionOperatorReferenceEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<RegionOperatorReferenceEntity> builder)
			{
				builder.HasQueryFilter(x =>
					x.RegionOperatorReferenceType == 14); // 14 - тип информационного контента - Лендинг авансирования
			}
		}
	}
}