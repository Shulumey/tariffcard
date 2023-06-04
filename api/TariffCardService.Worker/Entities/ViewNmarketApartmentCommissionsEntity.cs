using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using TariffCardService.Core.Enum;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Представление помещения со всеми уровнями комиссий на него(из View).
	/// </summary>
	[Table("viewNBAppartmentCommissions", Schema = "dbo")]
	public class ViewNMarketApartmentCommissionsEntity
	{
		/// <summary>
		/// Идентификатор помещения в базе НМаркет.
		/// </summary>
		[Key]
		[Column("AgencyNBAppartmentId")]
		public long ApartmentId { get; set; }

		/// <summary>
		/// Значение комиссии субагента на уровне продавца.
		/// </summary>
		[Column("SellerComissionValue", TypeName = "decimal(12, 2)")]
		public decimal? SellerLevelCommissionValue { get; set; }

		/// <summary>
		/// Тип комиссионных на уровне продавца.
		/// </summary>
		[Column("SellerComissionType")]
		public CommissionType? SellerLevelCommissionType { get; set; }

		/// <summary>
		/// Значение комиссии субагента на уровне корпуса.
		/// </summary>
		[Column("HouseComissionValue", TypeName = "decimal(12, 2)")]
		public decimal? HouseLevelCommissionValue { get; set; }

		/// <summary>
		/// Тип комиссионных на уровне корпуса.
		/// </summary>
		[Column("HouseComissionType")]
		public CommissionType? HouseLevelCommissionType { get; set; }

		/// <summary>
		/// Значение комиссии субагента на уровне помещения.
		/// </summary>
		[Column("AppartmentCommissionValue", TypeName = "decimal(12, 2)")]
		public decimal? ApartmentLevelCommissionValue { get; set; }

		/// <summary>
		/// Тип комиссионных на уровне помещения.
		/// </summary>
		[Column("AppartmentCommissionType")]
		public CommissionType? ApartmentLevelCommissionType { get; set; }

		/// <summary>
		/// Итоговое значение комиссии субагента на помещение.
		/// </summary>
		[Column("TotalCommissionValue", TypeName = "decimal(12, 2)")]
		public decimal TotalCommissionValue { get; set; }

		/// <summary>
		/// Итоговый тип комиссионных на помещение.
		/// </summary>
		[Column("TotalCommissionType")]
		public CommissionType TotalCommissionType { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="ViewNMarketApartmentCommissionsEntity"/>.
		/// </summary>
		public class ViewNMarketApartmentCommissionsConfiguration : IEntityTypeConfiguration<ViewNMarketApartmentCommissionsEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="ViewNMarketApartmentCommissionsEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<ViewNMarketApartmentCommissionsEntity> builder)
			{
				var converterCommissionType = new EnumToNumberConverter<CommissionType, byte>();

				builder.Property(item => item.TotalCommissionType).HasConversion(converterCommissionType);
				builder.Property(item => item.ApartmentLevelCommissionType).HasConversion(converterCommissionType);
				builder.Property(item => item.HouseLevelCommissionType).HasConversion(converterCommissionType);
				builder.Property(item => item.SellerLevelCommissionType).HasConversion(converterCommissionType);
			}
		}
	}
}