using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using TariffCardService.Core.Enum;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Объекты снимка данных тарифной карты уровня помещений.
	/// </summary>
	[Table("CommissionSnapshotApps", Schema = "brokerage")]
	public class CommissionSnapshotApartmentLevelEntity
	{
		/// <summary>
		/// Идентификатор объекта тарифной карты уровня помещений в базе НМаркет.
		/// </summary>
		[Key]
		[Column("Id")]
		public int Id { get; set; }

		/// <summary>
		/// Идентификатор снимка данных в каталоге снимков данных в базе НМаркет.
		/// </summary>
		[Column("SnapshotId")]
		public int SnapshotId { get; set; }

		/// <summary>
		/// Идентификатор застройщика в базе НМаркет.
		/// </summary>
		[Column("BuilderId")]
		public int BuilderId { get; set; }

		/// <summary>
		/// Наименование застройщика.
		/// </summary>
		[Column("BuilderName")]
		public string BuilderName { get; set; }

		/// <summary>
		/// Идентификатор помещения в базе НМаркет.
		/// </summary>
		[Column("AgencyNBAppartmentId")]
		public long ApartmentId { get; set; }

		/// <summary>
		/// Описание объекта недвижимости в базе НМаркет.
		/// </summary>
		[Column("AppartmentDescription")]
		public string ApartmentDescription { get; set; }

		/// <summary>
		/// Идентификатор корпуса в базе НМаркет.
		/// </summary>
		[Column("AgencyNBHouseId")]
		public long HouseId { get; set; }

		/// <summary>
		/// Идентификатор комплекса в базе НМаркет.
		/// </summary>
		[Column("ComplexId")]
		public long? ComplexId { get; set; }

		/// <summary>
		///  Комнатность помещения.
		/// </summary>
		[Column("Rooms")]
		public byte Rooms { get; set; }

		/// <summary>
		/// Идентификатор региона местонахождения помещения.
		/// </summary>
		[Column("RegionId")]
		public int RegionId { get; set; }

		/// <summary>
		/// Идентификатор региональной группы местонахождения помещения.
		/// </summary>
		[Column("RegionGroupId")]
		public int RegionGroupId { get; set; }

		/// <summary>
		/// Признак, что работа с этой компанией предоставляется в рамках услуги "Расширенное бронирование".
		/// </summary>
		[Column("IsAdvancedBooking", TypeName = "bit")]
		public bool? IsAdvancedBooking { get; set; }

		/// <summary>
		/// Идентификатор компании-продавца, видимой для субагентов.
		/// </summary>
		[Column("PublicSellerId")]
		public int? PublicSellerId { get; set; }

		/// <summary>
		/// Идентификатор статуса помещения.
		/// </summary>
		[Column("AppartmentStatusId")]
		public ObjectStatus ApartmentStatus { get; set; }

		/// <summary>
		/// Наименование комплекса.
		/// </summary>
		[Column("HouseName")]
		public string ComplexName { get; set; }

		/// <summary>
		/// Тип продавца.
		/// </summary>
		[Column("SellerType", TypeName = "smallint")]
		public SellerType SellerType { get; set; }

		/// <summary>
		/// Уровень назначения комиссий.
		/// </summary>
		[Column("CommissionLevel")]
		public CommissionLevelType CommissionLevel { get; set; }

		/// <summary>
		/// Тип комиссионных субагента.
		/// </summary>
		[Column("CommissionType")]
		public CommissionType CommissionType { get; set; }

		/// <summary>
		/// Значение комиссиионных субагента.
		/// </summary>
		[Column("CommissionValue", TypeName = "decimal(18,2)")]
		public decimal CommissionValue { get; set; }

		/// <summary>
		/// Тип помещения.
		/// </summary>
		[Column("TTypeObjNewBuildId", TypeName = "smallint")]
		public RealtyObjectType RealtyObjectType { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="CommissionSnapshotApartmentLevelEntity"/>.
		/// </summary>
		public class CommissionSnapshotApartmentLevelConfiguration : IEntityTypeConfiguration<CommissionSnapshotApartmentLevelEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="CommissionSnapshotApartmentLevelEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<CommissionSnapshotApartmentLevelEntity> builder)
			{
				var converterRealtyObjectType = new EnumToNumberConverter<RealtyObjectType, short>();
				var converterCommissionLevelType = new EnumToNumberConverter<CommissionLevelType, byte>();
				var converterObjectStatus = new EnumToNumberConverter<ObjectStatus, short>();
				var converterCommissionType = new EnumToNumberConverter<CommissionType, byte>();
				var converterSellerType = new EnumToNumberConverter<SellerType, short>();

				builder.Property(item => item.SellerType).HasConversion(converterSellerType);
				builder.Property(item => item.RealtyObjectType).HasConversion(converterRealtyObjectType);
				builder.Property(item => item.CommissionLevel).HasConversion(converterCommissionLevelType);
				builder.Property(item => item.CommissionType).HasConversion(converterCommissionType);
				builder.Property(item => item.ApartmentStatus).HasConversion(converterObjectStatus);
			}
		}
	}
}