using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TariffCardService.DataAccess.Entities;

namespace TariffCardService.DataAccess.EntityConfiguration
{
	/// <summary>
	/// Конфигурация для типа сущности <see cref="HouseSnapshot"/>.
	/// </summary>
	public class HouseSnapshotConfiguration : IEntityTypeConfiguration<HouseSnapshot>
	{
		/// <summary>
		/// Настройка объекта снимка данных в тип <see cref="HouseSnapshot"/>.
		/// </summary>
		/// <param name="builder">Объект, который нужно настроить.</param>
		public void Configure(EntityTypeBuilder<HouseSnapshot> builder)
		{
			builder.ToTable("HouseSnapshots");
			builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
			builder.HasIndex(x => x.Id);
			builder.Property(x => x.CommissionType).HasColumnName("CommissionType");
			builder.Property(x => x.CommissionValue).HasColumnName("CommissionValue");
			builder.Property(x => x.HasOverriding).HasColumnName("HasOverriding").IsRequired();
			builder.Property(x => x.HouseId).HasColumnName("HouseId");
			builder.Property(x => x.HouseName).HasColumnName("HouseName").IsRequired();
			builder.Property(x => x.ObjectsCount).HasColumnName("ObjectsCount").IsRequired();
			builder.Property(x => x.ComplexSnapshotId).HasColumnName("ComplexSnapshotId").IsRequired();
			builder.Property(x => x.MaxCommissionValue).HasColumnName("MaxCommissionValue").IsRequired();
			builder.Property(x => x.MinCommissionValue).HasColumnName("MinCommissionValue").IsRequired();
			builder.Property(x => x.RealtyObjectType).HasColumnName("RealtyObjectType").IsRequired();
			builder.Property(x => x.MinMaxCommissionType).HasColumnName("MinMaxCommissionType").IsRequired();
			builder.Property(x => x.CrossRegionAdvancedBookingCoefficient).HasColumnName("CrossRegionAdvancedBookingCoefficient").IsRequired();
			builder.HasMany(x => x.ObjectGroups).WithOne().HasForeignKey(x => x.HouseSnapshotId);
			builder.HasIndex("HouseId", "HouseName", "RealtyObjectType");
		}
	}
}