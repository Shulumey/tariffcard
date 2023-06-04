using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Тарифные зоны для регионов.
	/// </summary>
	[Table("TariffZoneRegions", Schema = "brokerage")]
	public class TariffZoneRegionsEntity
	{
		/// <summary>
		/// Идентификатор записи о тарифной зоне.
		/// </summary>
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		/// <summary>
		/// Идентификатор региона.
		/// </summary>
		[Column("RegionId")]
		public int RegionId { get; set; }

		/// <summary>
		/// Коэффициент кросс-региональной сделки в этом регионе.
		/// </summary>
		[Column("Coefficient", TypeName = "decimal(4, 2)")]
		public decimal Coefficient { get; set; }

		/// <summary>
		/// Признак применения этого коэффициент по умолчанию для данного региона.
		/// </summary>
		[Column("IsDefault")]
		public bool? IsDefault { get; set; }

		/// <summary>
		/// Дата создания.
		/// </summary>
		[Column("CreateDate", TypeName = "smalldatetime")]
		public DateTime? CreateDate { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="TariffZoneRegionsEntity"/>.
		/// </summary>
		public class TariffZoneRegionsConfiguration : IEntityTypeConfiguration<TariffZoneRegionsEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="TariffZoneRegionsEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<TariffZoneRegionsEntity> builder)
			{
			}
		}
	}
}