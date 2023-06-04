using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TariffCardService.Worker.Entities
{
	/// <summary>
	/// Адресная система - дерево адресов, от улиц до регионов.
	/// </summary>
	[Table("AddressObj", Schema = "dbo")]
	public class AddressObjectEntity
	{
		/// <summary>
		/// Идентификатор адреса.
		/// </summary>
		[Key]
		[Column("ObjectId")]
		public int ObjectId { get; set; }

		/// <summary>
		/// Региональная группа.
		/// </summary>
		[Column("RegionGroupId")]
		public int RegionGroupId { get; set; }

		/// <summary>
		/// Конфигурация для типа сущности <see cref="AddressObjectEntity"/>.
		/// </summary>
		public class AddressObjectConfiguration : IEntityTypeConfiguration<AddressObjectEntity>
		{
			/// <summary>
			/// Настройка объекта адресной системы в тип <see cref="AddressObjectEntity"/>.
			/// </summary>
			/// <param name="builder">Объект, который нужно настроить.</param>
			public void Configure(EntityTypeBuilder<AddressObjectEntity> builder)
			{
			}
		}
	}
}