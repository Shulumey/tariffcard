using Microsoft.EntityFrameworkCore;

using TariffCardService.Worker.Entities;
using TariffCardService.Worker.Interfaces;

namespace TariffCardService.Worker.Infrastructure
{
	/// <summary>
	/// Представляет сеанс работы с базой данных тарифной карты NMarket.
	/// </summary>
	internal class TariffCardCommonDbContext : DbContext, ITariffCardCommonDbContext
	{
		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="TariffCardCommonDbContext"/>.
		/// </summary>
		/// <param name="options">DbContextOptions&lt;TariffCardCommonDbContext&gt;>.</param>
		public TariffCardCommonDbContext(DbContextOptions<TariffCardCommonDbContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// Представление снимка данных тарифной карты всех уровней, кроме помещений.
		/// </summary>
		public DbSet<CommissionSnapshotCommonLevelEntity> CommissionSnapshotCommonLevel { get; set; }

		/// <summary>
		/// Представление снимка данных тарифной карты уровня помещений.
		/// </summary>
		public DbSet<CommissionSnapshotApartmentLevelEntity> CommissionSnapshotApartmentLevel { get; set; }

		/// <summary>
		/// Представление каталога со снимками данных тарифной карты.
		/// </summary>
		public DbSet<CommissionSnapshotCatalogEntity> CommissionSnapshotCatalog { get; set; }

		/// <summary>
		/// Представление правил предоставления услуг по проведениям сделок для связи оператор/продавец.
		/// </summary>
		public DbSet<OperatorSellerConditionsEntity> OperatorSellerConditions { get; set; }

		/// <summary>
		/// Представление авансированных продавцов.
		/// </summary>
		public DbSet<SellerCommissionPrepaymentsEntity> SellerCommissionPrepayments { get; set; }

		/// <summary>
		/// Представление помещений всех типов (квартиры, апартаменты, коммерция, машиноместа, кладовки) + их мастер-помещения.
		/// </summary>
		public DbSet<NMarketApartmentLocalEntity> NMarketApartmentLocal { get; set; }

		/// <summary>
		/// Представление домов/корпусов и комплексов.
		/// </summary>
		public DbSet<NMarketHouseLocalEntity> NMarketHouseLocal { get; set; }

		/// <summary>
		/// Представление сопоставлений настоящих и публичных продавцов помещения.
		/// </summary>
		public DbSet<ViewNMarketApartmentComparisonSellersEntity> ViewNMarketApartmentComparisonSellers { get; set; }

		/// <summary>
		/// Представление компаний-продавцов.
		/// </summary>
		public DbSet<NMarketBuilderPropertiesEntity> NMarketBuilderProperties { get; set; }

		/// <summary>
		/// Представление помещений со всеми уровнями комиссий на него.
		/// </summary>
		public DbSet<ViewNMarketApartmentCommissionsEntity> ViewNMarketApartmentCommissions { get; set; }

		/// <summary>
		/// Представление адреснаой системы - дерево адресов, от улиц до регионов.
		/// </summary>
		public DbSet<AddressObjectEntity> AddressObject { get; set; }

		/// <summary>
		/// Представление тарифных зон для регионов.
		/// </summary>
		public DbSet<TariffZoneRegionsEntity> TariffZoneRegions { get; set; }

		/// <summary>
		/// Переопределение кросс-регионального коэффициента.
		/// </summary>
		public DbSet<TariffZoneRegionsOverridingEntity> TariffZoneRegionsOverriding { get; set; }

		/// <summary>
		/// Представление сопоставления регионального оператора и его региональной группы.
		/// </summary>
		public DbSet<ViewRegionalOperatorsBasePropertiesEntity> ViewRegionalOperatorsBaseProperties { get; set; }

		/// <summary>
		/// Представление ссылок на информационные ресурсы региональных операторов.
		/// </summary>
		public DbSet<RegionOperatorReferenceEntity> RegionOperatorReference { get; set; }

		/// <inheritdoc />
		public DbSet<SearchParameterAliasEntity> SearchParamAliases { get; set; }

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(TariffCardCommonDbContext).Assembly);
		}
	}
}