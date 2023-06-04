using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TariffCardService.DataAccess.Entities;

namespace TariffCardService.DataAccess.EntityConfiguration
{
	/// <summary>
	/// Конфигурация для типа сущности <see cref="CommissionObjectGroup"/>.
	/// </summary>
	public class CommissionObjectGroupConfiguration : IEntityTypeConfiguration<CommissionObjectGroup>
	{
		/// <summary>
		/// Настройка объекта снимка данных в тип <see cref="CommissionObjectGroup"/>.
		/// </summary>
		/// <param name="builder">Объект, который нужно настроить.</param>
		public void Configure(EntityTypeBuilder<CommissionObjectGroup> builder)
		{
			builder.ToTable("CommissionObjectGroups");
			builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Rooms).HasColumnName("Rooms");
			builder.Property(x => x.ApartmentDescription).HasColumnName("ApartmentDescription").IsRequired();
			builder.Property(x => x.ApartmentId).HasColumnName("ApartmentId");
			builder.Property(x => x.CommissionType).HasColumnName("CommissionType").IsRequired();
			builder.Property(x => x.CommissionValue).HasColumnName("CommissionValue").IsRequired();
			builder.Property(x => x.IsOverriding).HasColumnName("IsOverriding").IsRequired();
			builder.Property(x => x.HouseGroupId).HasColumnName("CommissionHouseGroupId").IsRequired();
			builder.Property(x => x.RealtyObjectType).HasColumnName("RealtyObjectType").IsRequired();
			builder.Property(x => x.CrossRegionAdvancedBookingCoefficient).HasColumnName("CrossRegionAdvancedBookingCoefficient").IsRequired();
			builder.HasIndex("ApartmentDescription", "ApartmentId", "RealtyObjectType");
		}
	}
}