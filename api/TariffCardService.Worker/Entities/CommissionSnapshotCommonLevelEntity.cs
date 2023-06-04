using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using TariffCardService.Core.Enum;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Объекты Снимков данных тарифной карты всех уровней, кроме помещений.
	/// </summary>
	[Table("CommissionSnapshotCommon", Schema = "brokerage")]
	public class CommissionSnapshotCommonLevelEntity
	{
		/// <summary>
		/// Идентификатор объекта тарифной карты всех уровней, кроме помещений, в базе НМаркет.
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
		/// Идентификатор комплекса в базе НМаркет.
		/// </summary>
		[Column("ComplexId")]
		public long? ComplexId { get; set; }

		/// <summary>
		/// Наименование комплекса.
		/// </summary>
		[Column("HouseName")]
		public string ComplexName { get; set; }

		/// <summary>
		/// Идентификатор региона объекта недвижимости.
		/// </summary>
		[Column("RegionId")]
		public int RegionId { get; set; }

		/// <summary>
		/// Идентификатор региональной группы объекта недвижимости.
		/// </summary>
		[Column("RegionGroupId")]
		public int RegionGroupId { get; set; }

		/// <summary>
		/// Тип объекта тарифной карты всех уровней, кроме помещений.
		/// </summary>
		[Column("ObjHouseType", TypeName = "smallint")]
		public ObjectType ObjectHouseType { get; set; }

		/// <summary>
		/// Тип продавца.
		/// </summary>
		[Column("SellerType", TypeName = "smallint")]
		public SellerType SellerType { get; set; }

		/// <summary>
		/// Уровень назначения комиссий.
		/// </summary>
		[Column("CommissionLevel", TypeName = "tinyint")]
		public CommissionLevelType CommissionLevel { get; set; }

		/// <summary>
		/// Тип комиссионных субагента.
		/// </summary>
		[Column("CommissionType", TypeName = "tinyint")]
		public CommissionType CommissionType { get; set; }

		/// <summary>
		/// Значение комиссиионных субагента.
		/// </summary>
		[Column("CommissionValue", TypeName = "decimal(18,2)")]
		public decimal CommissionValue { get; set; }

		/// <summary>
		/// Комнатность объекта недвижимости.
		/// </summary>
		[Column("Rooms")]
		public byte Rooms { get; set; }

		/// <summary>
		/// Идентификатор корпуса в базе НМаркет.
		/// </summary>
		[Column("AgencyNBHouseId")]
		public long HouseId { get; set; }

		/// <summary>
		/// Наименование корпуса в базе НМаркет.
		/// </summary>
		[Column("HouseDescription")]
		public string HouseDescription { get; set; }

		/// <summary>
		/// Признак, что работа с этой компанией предоставляется в рамках услуги "Расширенное бронирование".
		/// </summary>
		[Column("IsAdvancedBooking", TypeName = "bit")]
		public bool? IsAdvancedBooking { get; set; }

		/// <summary>
		/// Признак, что комиссия считается от цены при полной оплате.
		/// </summary>
		[Column("IsComissionSourceFrom100Percent", TypeName = "bit")]
		public bool? IsCommissionCalculatedFromTotalPrice { get; set; }

		/// <summary>
		/// Тип помещения объекта недвижимости.
		/// </summary>
		[Column("TTypeObjNewBuildId", TypeName = "smallint")]
		public RealtyObjectType? RealtyObjectType { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="CommissionSnapshotCommonLevelEntity"/>.
		/// </summary>
		public class CommissionSnapshotCommonLevelConfiguration : IEntityTypeConfiguration<CommissionSnapshotCommonLevelEntity>
		{
			/// <summary>
			/// Настройка объекта снимков данных в тип <see cref="CommissionSnapshotCommonLevelEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<CommissionSnapshotCommonLevelEntity> builder)
			{
				var converterRealtyObjectType = new EnumToNumberConverter<RealtyObjectType, short>();
				var converterCommissionLevelType = new EnumToNumberConverter<CommissionLevelType, byte>();
				var converterObjectType = new EnumToNumberConverter<ObjectType, short>();
				var converterCommissionType = new EnumToNumberConverter<CommissionType, byte>();
				var converterSellerType = new EnumToNumberConverter<SellerType, short>();

				builder.Property(item => item.SellerType).HasConversion(converterSellerType);
				builder.Property(item => item.RealtyObjectType).HasConversion(converterRealtyObjectType);
				builder.Property(item => item.CommissionLevel).HasConversion(converterCommissionLevelType);
				builder.Property(item => item.CommissionType).HasConversion(converterCommissionType);
				builder.Property(item => item.ObjectHouseType).HasConversion(converterObjectType);
			}
		}
	}
}