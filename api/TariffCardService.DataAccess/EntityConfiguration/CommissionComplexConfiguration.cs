using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TariffCardService.DataAccess.Entities;

namespace TariffCardService.DataAccess.EntityConfiguration
{
	/// <summary>
	/// Конфигурация для типа сущности <see cref="CommissionComplex"/>.
	/// </summary>
	public class CommissionComplexConfiguration : IEntityTypeConfiguration<CommissionComplex>
	{
		/// <summary>
		/// Настройка объекта снимка данных в тип <see cref="CommissionComplex"/>.
		/// </summary>
		/// <param name="builder">Объект, который нужно настроить.</param>
		public void Configure(EntityTypeBuilder<CommissionComplex> builder)
		{
			builder.ToTable("CommissionComplexes");
			builder.Property(x => x.CommissionType).HasColumnName("CommissionType");
			builder.Property(x => x.ComplexId).HasColumnName("ComplexId").IsRequired();
			builder.Property(x => x.ComplexName).HasColumnName("ComplexName").IsRequired();
			builder.Property(x => x.HousesCount).HasColumnName("HousesCount").IsRequired();
			builder.Property(x => x.SellerId).HasColumnName("SellerId").IsRequired();
			builder.Property(x => x.SellerName).HasColumnName("SellerName").IsRequired();
			builder.Property(x => x.SellerType).HasColumnName("SellerType").IsRequired();
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
			builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
			builder.HasKey(x => x.Id);
			builder.HasIndex("ComplexId", "RealtyObjectType", "SellerId", "SellerType");
			builder.HasMany(x => x.HouseGroups).WithOne().HasForeignKey(x => x.ComplexId);
		}
	}
}