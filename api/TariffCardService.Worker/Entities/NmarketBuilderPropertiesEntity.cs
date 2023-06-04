using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Свойства компании-продавца (из View).
	/// </summary>
	[Table("NBBuilder", Schema = "dbo")]
	public class NMarketBuilderPropertiesEntity
	{
		/// <summary>
		/// Идентификатор компании в базе НМаркет.
		/// </summary>
		[Key]
		[Column("BuilderId")]
		public int BuilderId { get; set; }

		/// <summary>
		/// Наименование компании.
		/// </summary>
		[Column("Name")]
		public string Name { get; set; }

		/// <summary>
		/// Признак, что можно для бронирования продуктов компании вставать в очередь.
		/// </summary>
		[Column("HasQueue")]
		public bool HasQueue { get; set; }

		/// <summary>
		/// Признак, что комиссия считается от цены при полной оплате.
		/// </summary>
		[Column("IsComissionSourceFrom100Percent")]
		public bool IsCommissionCalculatedFromTotalPrice { get; set; }

		/// <summary>
		/// Признак, что работа с этой компанией предоставляется в рамках услуги "Расширенное бронирование".
		/// </summary>
		[Column("IsAdvancedBooking")]
		public bool IsAdvancedBooking { get; set; }

		/// <summary>
		/// Идентификатор региона местонахождения компании.
		/// </summary>
		[ForeignKey("Region")]
		[Column("RegionId")]
		public int RegionId { get; set; }

		/// <inheritdoc cref="AddressObjectEntity"/>
		public AddressObjectEntity Region { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="NMarketBuilderPropertiesEntity"/>.
		/// </summary>
		public class NMarketBuilderPropertiesConfiguration : IEntityTypeConfiguration<NMarketBuilderPropertiesEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="NMarketBuilderPropertiesEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<NMarketBuilderPropertiesEntity> builder)
			{
			}
		}
	}
}