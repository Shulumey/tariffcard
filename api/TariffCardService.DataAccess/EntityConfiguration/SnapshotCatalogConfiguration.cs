using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TariffCardService.Core.Models;

namespace TariffCardService.DataAccess.EntityConfiguration
{
	/// <summary>
	/// Конфигурация для типа сущности <see cref="SnapshotCatalog"/>.
	/// </summary>
	public class SnapshotCatalogConfiguration : IEntityTypeConfiguration<Entities.SnapshotCatalog>
	{
		/// <summary>
		/// Настройка объекта снимка данных в тип <see cref="SnapshotCatalog"/>.
		/// </summary>
		/// <param name="builder">Объект, который нужно настроить.</param>
		public void Configure(EntityTypeBuilder<Entities.SnapshotCatalog> builder)
		{
			builder.ToTable("SnapshotCatalog");
			builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Type).HasColumnName("Type").IsRequired();
			builder.Property(x => x.Date).HasColumnName("Date").IsRequired();
			builder.HasMany(x => x.Complexes).WithOne().HasForeignKey(x => x.SnapshotId);
			builder.HasIndex(x => x.Date).IsUnique();
		}
	}
}