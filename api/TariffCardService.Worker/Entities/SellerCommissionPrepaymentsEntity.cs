using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using TariffCardService.Core.Enum;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	///  Авансированные продавцы.
	/// </summary>
	[Table("SellerCommissionPrepayments", Schema = "dbo")]
	public class SellerCommissionPrepaymentsEntity
	{
		/// <summary>
		/// Идентификатор записи.
		/// </summary>
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		/// <summary>
		/// Идентификатор продавца в базе НМаркет.
		/// </summary>
		[Column("SellerId")]
		public int SellerId { get; set; }

		/// <summary>
		/// Тип продавца.
		/// </summary>
		[Column("SellerType")]
		public SellerType SellerType { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="SellerCommissionPrepaymentsEntity"/>.
		/// </summary>
		public class SellerCommissionPrepaymentsCatalogConfiguration : IEntityTypeConfiguration<SellerCommissionPrepaymentsEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="SellerCommissionPrepaymentsEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<SellerCommissionPrepaymentsEntity> builder)
			{
				var converterSellerType = new EnumToNumberConverter<SellerType, short>();

				builder.Property(item => item.SellerType).HasConversion(converterSellerType);
			}
		}
	}
}