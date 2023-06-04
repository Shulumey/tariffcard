using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Объекты каталога со снимками данных тарифной карты.
	/// </summary>
	[Table("CommissionSnapshotCatalog", Schema = "brokerage")]
	public class CommissionSnapshotCatalogEntity
	{
		/// <summary>
		/// Идентификатор снимка данных.
		/// </summary>
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		/// <summary>
		/// Дата снимка данных.
		/// </summary>
		[Column("date", TypeName = "datetime")]
		public DateTime Date { get; set; }

		/// <summary>
		/// Тип снимка данных.
		/// </summary>
		[Column("Type")]
		public short Type { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="CommissionSnapshotCatalogEntity"/>.
		/// </summary>
		public class CommissionSnapshotCatalogConfiguration : IEntityTypeConfiguration<CommissionSnapshotCatalogEntity>
		{
			/// <summary>
			/// Настройка объекта каталога снимка данных в тип <see cref="CommissionSnapshotCatalogEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<CommissionSnapshotCatalogEntity> builder)
			{
			}
		}
	}
}