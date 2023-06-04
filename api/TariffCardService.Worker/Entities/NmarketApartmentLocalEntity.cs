using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using TariffCardService.Core.Enum;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Помещения всех типов (квартиры, апартаменты, коммерция, машиноместа, кладовки) + их мастер-помещения.
	/// </summary>
	[Table("NBAppartmentLocal", Schema = "dbo")]
	public class NMarketApartmentLocalEntity
	{
		/// <summary>
		/// Идентификатор квартиры в базе НМаркет.
		/// </summary>
		[Key]
		[Column("AgencyNBAppartmentId")]
		public long ApartmentId { get; set; }

		/// <summary>
		/// Идентификатор дома в базе НМаркет.
		/// </summary>
		[Column("AgencyNBHouseId")]
		public long? HouseId { get; set; }

		/// <summary>
		/// Идентификатор статуса помещения.
		/// </summary>
		[Column("TStatusId")]
		public ObjectStatus? ApartmentStatus { get; set; }

		/// <summary>
		/// Комнатность помещения.
		/// </summary>
		[Column("Rooms")]
		public short Rooms { get; set; }

		/// <summary>
		/// Общая площадь помещения.
		/// </summary>
		[Column("SAll", TypeName = "decimal(8, 2)")]
		public decimal SquareTotal { get; set; }

		/// <summary>
		/// Признак, что данное помещение удалено.
		/// </summary>
		[Column("Deleted")]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Признак несуществующего помещения.
		/// </summary>
		[Column("IsFake")]
		public bool IsFake { get; set; }

		/// <summary>
		/// Признак, что помещение является студией.
		/// </summary>
		[Column("IsStudio")]
		public bool IsStudio { get; set; }

		/// <summary>
		/// Признак, что помещение является помещением-шаблоном(мастер-объектом).
		/// </summary>
		[Column("IsMasterApp")]
		public bool IsMasterApp { get; set; }

		/// <summary>
		/// Номер помещения в доме.
		/// </summary>
		[Column("AppNumber")]
		public string ApartmentNumber { get; set; }

		/// <summary>
		/// Идентификатор типа продавца.
		/// </summary>
		[Column("TSellerTypeId")]
		public SellerType SellerType { get; set; }

		/// <summary>
		/// Идентификатор продавца в базе НМаркет.
		/// </summary>
		[Column("SellerContractorId")]
		public int? SellerContractorId { get; set; }

		/// <summary>
		/// Имя продавца.
		/// </summary>
		[Column("SellerName")]
		public string SellerName { get; set; }

		/// <summary>
		/// Идентификатор региона местонахождения помещения.
		/// </summary>
		[Column("RegionId")]
		public int RegionId { get; set; }

		/// <summary>
		/// Тип помещения.
		/// </summary>
		[Column("TTypeObjNewBuildId")]
		public RealtyObjectType RealtyObjectType { get; set; }

		/// <summary>
		/// Тип квартиры у застройщика, символьно цифровой идентификатор.
		/// </summary>
		[Column("Type")]
		public string Type { get; set; }

		/// <inheritdoc cref="ViewNMarketApartmentComparisonSellersEntity"/>
		[ForeignKey("ApartmentId")]
		public ViewNMarketApartmentComparisonSellersEntity ViewNMarketApartmentComparisonSellers { get; set; }

		/// <inheritdoc cref="ViewNMarketApartmentCommissionsEntity"/>
		[ForeignKey("ApartmentId")]
		public ViewNMarketApartmentCommissionsEntity ViewNMarketApartmentCommissions { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="NMarketApartmentLocalEntity"/>.
		/// </summary>
		public class NMarketApartmentLocalConfiguration : IEntityTypeConfiguration<NMarketApartmentLocalEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="NMarketApartmentLocalEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<NMarketApartmentLocalEntity> builder)
			{
				var converterRealtyObjectType = new EnumToNumberConverter<RealtyObjectType, short>();
				var converterObjectStatus = new EnumToNumberConverter<ObjectStatus, short>();
				var converterSellerType = new EnumToNumberConverter<SellerType, short>();

				builder.Property(item => item.SellerType).HasConversion(converterSellerType);
				builder.Property(item => item.RealtyObjectType).HasConversion(converterRealtyObjectType);
				builder.Property(item => item.ApartmentStatus).HasConversion(converterObjectStatus);

				builder.HasQueryFilter(apartmentLocal =>
					!(apartmentLocal.IsMasterApp || apartmentLocal.IsDeleted || apartmentLocal.IsFake) &&
					apartmentLocal.ViewNMarketApartmentComparisonSellers.RealSellerPropertiesId == apartmentLocal.ViewNMarketApartmentComparisonSellers.PublicSellerPropertiesId &&
					(apartmentLocal.ApartmentStatus == ObjectStatus.Active ||
					 (apartmentLocal.ApartmentStatus == ObjectStatus.Reserved &&
					  apartmentLocal.ViewNMarketApartmentComparisonSellers.SellerProperties.HasQueue)));
			}
		}
	}
}