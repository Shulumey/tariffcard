using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TariffCardService.DataAccess.Entities;

namespace TariffCardService.DataAccess.EntityConfiguration
{
	/// <summary>
	/// Конфигурация для типа сущности <see cref="SearchParamAlias"/>.
	/// </summary>
	public class SearchParamAliasConfiguration : IEntityTypeConfiguration<SearchParamAlias>
	{
		/// <summary>
		/// Настройка объекта снимка данных в тип <see cref="ObjectSnapshot"/>.
		/// </summary>
		/// <param name="builder">Объект, который нужно настроить.</param>
		public void Configure(EntityTypeBuilder<SearchParamAlias> builder)
		{
			builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Alias).HasColumnName("Alias");
			builder.Property(x => x.Value).HasColumnName("DisplayName");
			builder.Property(x => x.RegionalGroupId).HasColumnName("RegionalGroupId");
		}
	}
}