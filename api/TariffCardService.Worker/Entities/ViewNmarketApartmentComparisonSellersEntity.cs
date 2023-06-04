using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Сопоставление настоящих и публичных продавцов помещения(из View).
	/// </summary>
	[Table("viewNBAppartmentSellers", Schema = "brokerage")]
	public class ViewNMarketApartmentComparisonSellersEntity
	{
		/// <summary>
		/// Идентификатор помещения в базе НМаркет.
		/// </summary>
		[Key]
		[Column("AgencyNBAppartmentId")]
		public long ApartmentId { get; set; }

		/// <summary>
		/// Идентификатор компании - настоящего продавца помещения.
		/// </summary>
		[Column("RealSellerId")]
		public int? RealSellerPropertiesId { get; set; }

		/// <summary>
		/// Идентификатор компании - отображаемого субагентам продавца помешения.
		/// </summary>
		[ForeignKey("SellerProperties")]
		[Column("PublicSellerId")]
		public int? PublicSellerPropertiesId { get; set; }

		/// <inheritdoc cref="NMarketBuilderPropertiesEntity"/>
		public NMarketBuilderPropertiesEntity SellerProperties { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="ViewNMarketApartmentComparisonSellersEntity"/>.
		/// </summary>
		public class ViewNmarketApartmentComparisonSellersConfiguration : IEntityTypeConfiguration<ViewNMarketApartmentComparisonSellersEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="ViewNMarketApartmentComparisonSellersEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<ViewNMarketApartmentComparisonSellersEntity> builder)
			{
			}
		}
	}
}