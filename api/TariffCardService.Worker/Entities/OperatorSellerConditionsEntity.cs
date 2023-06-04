using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Правила предоставления услуг по проведениям сделок для связи оператор/продавец.
	/// </summary>
	[Table("OperatorSellerConditions", Schema = "brokerage")]
	public class OperatorSellerConditionsEntity
	{
		/// <summary>
		/// Идентификатор компании-продавца в базе НМаркет.
		/// </summary>
		[Key]
		[Column("SellerId")]
		public int SellerId { get; set; }

		/// <summary>
		/// Условия выплаты комиссионных субагенту.
		/// </summary>
		[Column("ConditionsOfPaymentFees")]
		public string ConditionsOfPaymentFees { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="OperatorSellerConditionsEntity"/>.
		/// </summary>
		public class OperatorSellerConditionsConfiguration : IEntityTypeConfiguration<OperatorSellerConditionsEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="OperatorSellerConditionsEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<OperatorSellerConditionsEntity> builder)
			{
			}
		}
	}
}