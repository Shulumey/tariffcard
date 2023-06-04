using Microsoft.EntityFrameworkCore;

using TariffCardService.DataAccess.Entities;
using TariffCardService.DataAccess.Interfaces;

namespace TariffCardService.DataAccess.Infrastructure
{
	/// <summary>
	/// Представляет сеанс работы с базой данных тарфной карты.
	/// </summary>
	public class TariffCardDbContext : DbContext, ITariffCardDbContext
	{
		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="TariffCardDbContext"/>.
		/// </summary>
		/// <param name="options"> DbContextOptions&lt;TariffCardDbContext&gt;>.</param>
		public TariffCardDbContext(DbContextOptions<TariffCardDbContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// Снимок данных о комплексах, домах и объектах в них, на определенную дату.
		/// </summary>
		public DbSet<SnapshotCatalog> SnapshotsCatalog { get; set; }

		/// <summary>
		/// Снимок данных представления объектов недвижимости.
		/// </summary>
		public DbSet<ObjectSnapshot> ObjectSnapshots { get; set; }

		/// <summary>
		/// Снимок данных представления корпуса с данными об установленной комиссии на него, типе объектов,
		/// максимальной и минимальной комиссии объектов в нём.
		/// </summary>
		public DbSet<HouseSnapshot> HouseSnapshots { get; set; }

		/// <summary>
		/// Снимок данных комплекса с данными о продавце, типе объектов,
		/// максимальной и минимальной комиссии в нём, и данными о корпусах внутри него.
		/// </summary>
		public DbSet<ComplexSnapshot> ComplexSnapshots { get; set; }

		/// <summary>
		/// Представление объекта недвижимости.
		/// </summary>
		public DbSet<CommissionObjectGroup> CommissionObjects { get; set; }

		/// <summary>
		/// Представление корпуса с данными об установленной комиссии на него, типе объектов,
		/// максимальной и минимальной комиссии объектов в нём.
		/// </summary>
		public DbSet<CommissionHouseGroup> CommissionHouseGroups { get; set; }

		/// <summary>
		/// Представление комплекса с данными о продавце, типе объектов,
		/// максимальной и минимальной комиссии в нём, и данными о корпусах внутри него.
		/// </summary>
		public DbSet<CommissionComplex> CommissionComplexes { get; set; }

		/// <inheritdoc />
		public DbSet<SearchParamAlias> SearchParamAliases { get; set; }

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(TariffCardDbContext).Assembly);
		}
	}
}