using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TariffCardService.DataAccess.Entities;

namespace TariffCardService.DataAccess.EntityConfiguration
{
	/// <summary>
	/// Конфигурация для типа сущности <see cref="CommissionHouseGroup"/>.
	/// </summary>
	public class CommissionHouseGroupConfiguration : IEntityTypeConfiguration<CommissionHouseGroup>
	{
		/// <summary>
		/// Настройка объекта снимка данных в тип <see cref="CommissionHouseGroup"/>.
		/// </summary>
		/// <param name="builder">Объект, который нужно настроить.</param>
		public void Configure(EntityTypeBuilder<CommissionHouseGroup> builder)
		{
			builder.ToTable("CommissionHouseGroups");
			builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
			builder.HasKey(x => x.Id);
			builder.Property(x => x.CommissionType).HasColumnName("CommissionType");
			builder.Property(x => x.CommissionValue).HasColumnName("CommissionValue");
			builder.Property(x => x.ComplexId).HasColumnName("CommissionComplexId").IsRequired();
			builder.Property(x => x.HasOverriding).HasColumnName("HasOverriding").IsRequired();
			builder.Property(x => x.HouseId).HasColumnName("HouseId");
			builder.Property(x => x.HouseName).HasColumnName("HouseName").IsRequired();
			builder.Property(x => x.ObjectsCount).HasColumnName("ObjectsCount").IsRequired();
			builder.Property(x => x.MaxCommissionValue).HasColumnName("MaxCommissionValue");
			builder.Property(x => x.MinCommissionValue).HasColumnName("MinCommissionValue");
			builder.Property(x => x.RealtyObjectType).HasColumnName("RealtyObjectType").IsRequired();
			builder.Property(x => x.MinMaxCommissionType).HasColumnName("MinMaxCommissionType").IsRequired();
			builder.Property(x => x.CrossRegionAdvancedBookingCoefficient).HasColumnName("CrossRegionAdvancedBookingCoefficient").IsRequired();
			builder.HasMany(x => x.ObjectGroups).WithOne().HasForeignKey(x => x.HouseGroupId);
			builder.HasIndex("HouseId", "HouseName", "RealtyObjectType");
		}
	}
}