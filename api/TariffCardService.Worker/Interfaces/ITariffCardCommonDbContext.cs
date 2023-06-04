using Microsoft.EntityFrameworkCore;

using TariffCardService.Worker.Entities;

namespace TariffCardService.Worker.Interfaces
{
	/// <summary>
	/// Интерфейс для работы с базой данных снимков данных тарифной карты NMarket.
	/// </summary>
	internal interface ITariffCardCommonDbContext
    {
        /// <summary>
		/// Представление snapshot тарифной карты всех уровней, кроме помещений.
		/// </summary>
		DbSet<CommissionSnapshotCommonLevelEntity> CommissionSnapshotCommonLevel { get; }

		/// <summary>
		/// Представление snapshot тарифной карты уровня помещений.
		/// </summary>
		DbSet<CommissionSnapshotApartmentLevelEntity> CommissionSnapshotApartmentLevel { get; }

		/// <summary>
		/// Представление каталога snapshot тарифной карты.
		/// </summary>
		DbSet<CommissionSnapshotCatalogEntity> CommissionSnapshotCatalog { get; }

		/// <summary>
		/// Представление правил предоставления услуг по проведениям сделок для связи оператор/продавец.
		/// </summary>
		DbSet<OperatorSellerConditionsEntity> OperatorSellerConditions { get; }

		/// <summary>
		/// Представление авансированных продавцов.
		/// </summary>
		DbSet<SellerCommissionPrepaymentsEntity> SellerCommissionPrepayments { get; }

		/// <summary>
		/// Представление помещений всех типов (квартиры, апартаменты, коммерция, машиноместа, кладовки) + их мастер-помещения.
		/// </summary>
		DbSet<NMarketApartmentLocalEntity> NMarketApartmentLocal { get; }

		/// <summary>
		/// Представление домов/корпусов и комплексов.
		/// </summary>
		DbSet<NMarketHouseLocalEntity> NMarketHouseLocal { get; }

		/// <summary>
		/// Представление сопоставлений настоящих и публичных продавцов помещения.
		/// </summary>
		DbSet<ViewNMarketApartmentComparisonSellersEntity> ViewNMarketApartmentComparisonSellers { get; }

		/// <summary>
		/// Представление компаний-продавцов.
		/// </summary>
		DbSet<NMarketBuilderPropertiesEntity> NMarketBuilderProperties { get; }

		/// <summary>
		/// Представление помещений со всеми уровнями комиссий на него.
		/// </summary>
		DbSet<ViewNMarketApartmentCommissionsEntity> ViewNMarketApartmentCommissions { get; }

		/// <summary>
		/// Представление адреснаой системы - дерево адресов, от улиц до регионов.
		/// </summary>
		DbSet<AddressObjectEntity> AddressObject { get; }

		/// <summary>
		/// Представление тарифных зон для регионов.
		/// </summary>
		DbSet<TariffZoneRegionsEntity> TariffZoneRegions { get; }

		/// <summary>
		/// Переопределение кросс-регионального коэффициента.
		/// </summary>
		DbSet<TariffZoneRegionsOverridingEntity> TariffZoneRegionsOverriding { get; }

		/// <summary>
		/// Представление сопоставления регионального оператора и его региональной группы.
		/// </summary>
		DbSet<ViewRegionalOperatorsBasePropertiesEntity> ViewRegionalOperatorsBaseProperties { get; }

		/// <summary>
		/// Представление ссылок на информационные ресурсы региональных операторов.
		/// </summary>
		DbSet<RegionOperatorReferenceEntity> RegionOperatorReference { get; }

		/// <summary>
		/// Синонимы для поиска.
		/// </summary>
		DbSet<SearchParameterAliasEntity> SearchParamAliases { get; }
    }
}