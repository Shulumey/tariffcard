using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TariffCardService.DataAccess.Entities;

namespace TariffCardService.DataAccess.EntityConfiguration
{
	/// <summary>
	/// Конфигурация для типа сущности <see cref="ComplexSnapshot"/>.
	/// </summary>
	public class ComplexSnapshotConfiguration : IEntityTypeConfiguration<ComplexSnapshot>
	{
		/// <summary>
		/// Настройка объекта снимка данных в тип <see cref="ComplexSnapshot"/>.
		/// </summary>
		/// <param name="builder">Объект, который нужно настроить.</param>
		public void Configure(EntityTypeBuilder<ComplexSnapshot> builder)
		{
			builder.ToTable("ComplexSnapshots");
			builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
			builder.HasKey(x => x.Id);
			builder.Property(x => x.CommissionType).HasColumnName("CommissionType").IsRequired();
			builder.Property(x => x.ComplexId).HasColumnName("ComplexId").IsRequired();
			builder.Property(x => x.ComplexName).HasColumnName("ComplexName").IsRequired();
			builder.Property(x => x.HousesCount).HasColumnName("HousesCount").IsRequired();
			builder.Property(x => x.SellerId).HasColumnName("SellerId").IsRequired();
			builder.Property(x => x.SellerName).HasColumnName("SellerName").IsRequired();
			builder.Property(x => x.SellerType).HasColumnName("SellerType").IsRequired();
			builder.Property(x => x.SnapshotId).HasColumnName("SnapshotId").IsRequired();
			builder.Property(x => x.IsAdvancedBooking).HasColumnName("IsAdvancedBooking").IsRequired();
			builder.Property(x => x.MaxCommissionValue).HasColumnName("MaxCommissionValue");
			builder.Property(x => x.MinCommissionValue).HasColumnName("MinCommissionValue");
			builder.Property(x => x.RealtyObjectType).HasColumnName("RealtyObjectType").IsRequired();
			builder.Property(x => x.RegionGroupId).HasColumnName("RegionGroupId").IsRequired();
			builder.Property(x => x.ConditionsOfPaymentFees).HasColumnName("ConditionsOfPaymentFees");
			builder.Property(x => x.IsSellerCommissionPrepayments).HasColumnName("IsSellerCommissionPrepayments").IsRequired();
			builder.Property(x => x.UrlLandingPrepaymentBooking).HasColumnName("UrlLandingPrepaymentBooking");
			builder.Property(x => x.CrossRegionAdvancedBookingCoefficient).HasColumnName("CrossRegionAdvancedBookingCoefficient").IsRequired();
			builder.Property(x => x.IsCommissionCalculatedFromTotalPrice).HasColumnName("IsCommissionCalculatedFromTotalPrice").IsRequired();
			builder.HasMany(x => x.HouseSnapshots).WithOne().HasForeignKey(x => x.ComplexSnapshotId);
			builder.HasIndex("ComplexId", "RealtyObjectType", "SellerId", "SellerType");
		}
	}
}