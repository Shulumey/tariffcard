using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using TariffCardService.Core.Enum;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Дома/корпуса и комплексы.
	/// </summary>
	[Table("NBHouseLocal", Schema = "dbo")]
	public class NMarketHouseLocalEntity
	{
		/// <summary>
		/// Идентификатор дома/корпуса в базе НМаркет.
		/// </summary>
		[Key]
		[Column("AgencyNBHouseId")]
		public long HouseId { get; set; }

		/// <summary>
		/// Статус дома/корпуса.
		/// </summary>
		[Column("TStatusId")]
		public ObjectStatus StatusHouse { get; set; }

		/// <summary>
		/// Идентификатор региона местонахождения дома/корпуса.
		/// </summary>
		[Column("RegionId")]
		public int RegionId { get; set; }

		/// <summary>
		/// Название дома/корпуса(комплекса).
		/// </summary>
		[Column("NameHouse")]
		public string HouseName { get; set; }

		/// <summary>
		/// Признак, что дом/корпус(комплекс) удалён.
		/// </summary>
		[Column("Deleted")]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Очередь постройки дома/корпуса.
		/// </summary>
		[Column("StageNumber")]
		public short StageNumber { get; set; }

		/// <summary>
		/// Номер дома/корпуса.
		/// </summary>
		[Column("BuildingNumber")]
		public string BuildingNumber { get; set; }

		/// <summary>
		/// Идентификатор комплекса для этого дома/корпуса.
		/// </summary>
		[Column("ComplexId")]
		public long? ComplexId { get; set; }

		/// <summary>
		/// Признак, что дом/корпус является комплексом.
		/// </summary>
		[Column("IsComplex")]
		public bool IsComplex { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="NMarketHouseLocalEntity"/>.
		/// </summary>
		public class NMarketHouseLocalConfiguration : IEntityTypeConfiguration<NMarketHouseLocalEntity>
		{
			/// <summary>
			/// Настройка объекта в тип <see cref="NMarketHouseLocalEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<NMarketHouseLocalEntity> builder)
			{
				var converterObjectStatus = new EnumToNumberConverter<ObjectStatus, short>();
				builder.Property(item => item.StatusHouse).HasConversion(converterObjectStatus);

				builder.HasQueryFilter(houseLocal => !houseLocal.IsDeleted &&
				                                     (houseLocal.StatusHouse == ObjectStatus.Active ||
				                                      houseLocal.StatusHouse ==
				                                      ObjectStatus.PreBooking));
			}
		}
	}
}