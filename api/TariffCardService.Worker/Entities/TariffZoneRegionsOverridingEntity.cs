using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Переопределение кросс-регионального коэффициента.
	/// </summary>
	[Table("TariffZoneRegionsOverriding", Schema = "brokerage")]
	public class TariffZoneRegionsOverridingEntity
	{
		/// <summary>
		/// Идентификатор переопределения.
		/// </summary>
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		/// <summary>
		/// Идентификатор компании-покупателя(субагента).
		/// </summary>
		[Column("CompanyId")]
		public int CompanyId { get; set; }

		/// <summary>
		/// Идентификатор региона продавца.
		/// </summary>
		[Column("SellerRegionId")]
		public int SellerRegionId { get; set; }

		/// <summary>
		/// Коэффициент для комиссионных субагента в сделке.
		/// </summary>
		[Column("Coefficient", TypeName = "decimal(4, 2)")]
		public decimal? Coefficient { get; set; }

		/// <summary>
		/// Признак применения этого коэффициент по умолчанию для данного региона.
		/// </summary>
		[Column("IsDefault")]
		public bool? IsDefault { get; set; }

		/// <summary>
		/// Дата создания.
		/// </summary>
		[Column("DateCreate", TypeName = "date")]
		public DateTime DateCreate { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="TariffZoneRegionsOverridingEntity"/>.
		/// </summary>
		public class TariffZoneRegionsOverridingConfiguration : IEntityTypeConfiguration<TariffZoneRegionsOverridingEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="TariffZoneRegionsOverridingEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<TariffZoneRegionsOverridingEntity> builder)
			{
			}
		}
	}
}