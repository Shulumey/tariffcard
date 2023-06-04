using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using TariffCardService.Business.Features.Complexes.Command;
using TariffCardService.Core.Enum;
using TariffCardService.Core.Models;
using TariffCardService.Worker.Configurations;
using TariffCardService.Worker.Entities;
using TariffCardService.Worker.Extensions;
using TariffCardService.Worker.Factories;
using TariffCardService.Worker.Helpers;
using TariffCardService.Worker.Interfaces;

namespace TariffCardService.Worker.Jobs
{
	/// <inheritdoc />
	[DisallowConcurrentExecution]
	[UsedImplicitly]
	internal class LoadDataJob : IJob
	{
		/// <inheritdoc cref="ILogger"/>
		private readonly ILogger<LoadDataJob> _logger;

		/// <inheritdoc cref="ITariffCardCommonDbContext"/>
		private readonly ITariffCardCommonDbContext _dbContext;

		/// <inheritdoc cref="IMediator"/>
		private readonly IMediator _mediator;

		/// <summary>
		/// Cекция настройки для worker-а.
		/// </summary>
		private readonly IOptions<WorkerSettings> _workerSettings;

		/// <summary>
		/// Конструктор создания запрограммированной задачи в планировщике задач.
		/// </summary>
		/// <param name="logger"><see cref="ILogger"/>.</param>
		/// <param name="dbContext"><see cref="ITariffCardCommonDbContext"/>.</param>
		/// <param name="mediator"><see cref="IMediator"/>.</param>
		/// <param name="workerSettings">Cекция настройки для worker-а.</param>
		public LoadDataJob(
			ILogger<LoadDataJob> logger,
			ITariffCardCommonDbContext dbContext,
			IMediator mediator,
			IOptions<WorkerSettings> workerSettings)
		{
			_logger = logger;
			_dbContext = dbContext;
			_mediator = mediator;
			_workerSettings = workerSettings;
		}

		/// <inheritdoc />
		public async Task Execute(IJobExecutionContext executionContext)
		{
			_logger.LogInformation("Load Data job is running");
			var sWatch = new Stopwatch();
			sWatch.Start();

			var currentComplexes = await GetCurrentComplexes(executionContext.CancellationToken);
			await _mediator.Send(
				new UpdateComplexes.Command
				{
					Complexes = currentComplexes,
				}, executionContext.CancellationToken);

			_logger.LogInformation("Search aliases is sync running");

			await _mediator.Send(new SaveSearchParamsAliases.Command()
			{
				SearchParamAliases = _dbContext.SearchParamAliases
					.Where(x => x.RegionalGroupId != null)
					.Select(x => new SearchParamAlias
					{
						Id = x.Id,
						Alias = x.Alias,
						Value = x.ActualValue,
						RegionalGroupId = (int)x.RegionalGroupId,
					}).ToArray(),
			});

			_logger.LogInformation("Search aliases sync is completed");

			sWatch.Stop();
			_logger.LogInformation("Snapshot is created: {ElapsedMilliseconds}", sWatch.ElapsedMilliseconds);
		}

		/// <summary>
		/// Можно ли добавить объект в корпус.
		/// </summary>
		/// <param name="house">Корпус.</param>
		/// <param name="apartment">Oбъект.</param>
		/// <returns>.</returns>
		private static bool CanAddToObjects(HouseHelper house, ObjectHelper apartment) =>
			house.SellerId == apartment.SellerId &&
			house.SellerType == apartment.SellerType &&
			(house.RealtyObjectType == apartment.RealtyObjectType ||
			 (apartment.RealtyObjectType == RealtyObjectType.CommercialApartment && house.RealtyObjectType == RealtyObjectType.Apartment)) &&
			house.ObjectsInHouse.All(y =>
				y.ApartmentId != apartment.ApartmentId ||
				y.IsOverriding != apartment.IsOverriding ||
				y.ApartmentDescription !=
				apartment.ApartmentDescription);

		/// <summary>
		/// Получение данных о текущих комиссиях продавцов на помещения, корпуса и комплексы на уровне ЖК.
		/// </summary>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns><see cref="IReadOnlyCollection{T}"/> Данные о текущих комиссиях продавцов на помещения, корпуса и комплексы на уровне ЖК.</returns>
		private async Task<IReadOnlyCollection<Complex>> GetCurrentComplexes(CancellationToken cancellationToken)
		{
			var actualComplexes = new List<ComplexHelper>();
			var actualApartments = GetNMarketApartmentsQueryable();
			var operatorSellerConditions = await GetOperatorSellerConditions(cancellationToken);
			var sellerCommissionPrepayments = await GetSellerCommissionPrepayments(cancellationToken);
			var tariffsCrossRegion = await GetTariffZoneRegions(cancellationToken);
			var regionOperatorReferences = await GetRegionOperatorReferences(cancellationToken);
			var actualHousesAsync = GetNMarketHousesQueryable();
			var countHousesViewStart = 0;
			var countHousesViewFinish = 1000;
			var actualApartmentsLocal = actualApartments
				.Where(x => x.HouseId >= countHousesViewStart && x.HouseId <= countHousesViewFinish)
				.ToArray();
			var housesDb = actualHousesAsync.AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);

			while (await housesDb.MoveNextAsync())
			{
				var houseDb = housesDb.Current;

				if (houseDb.HouseId > countHousesViewFinish)
				{
					countHousesViewStart = countHousesViewFinish;
					countHousesViewFinish += 200;
					actualApartmentsLocal = actualApartments
						.Where(x => x.HouseId >= countHousesViewStart && x.HouseId <= countHousesViewFinish)
						.ToArray();
				}

				var apartmentsInHouseDb =
					actualApartmentsLocal.Where(x => x.HouseId == houseDb.HouseId).ToArray();
				if (apartmentsInHouseDb.Any())
				{
					var tariffsCrossRegionCoefficient = tariffsCrossRegion
						.FirstOrDefault(cr => cr.RegionId == houseDb.RegionId)?.Coefficient;
					var housesWithSellerInfo = apartmentsInHouseDb
						.Where(x => x.ViewNMarketApartmentComparisonSellers != null)
						.Select(x => HouseHelperFactory.Create(x, houseDb, tariffsCrossRegionCoefficient))
						.DistinctBy(x => new { x.HouseId, x.SellerId, x.SellerType, x.RealtyObjectType }).ToArray();

					foreach (var apartmentInHouse in apartmentsInHouseDb)
					{
						var apartment = ObjectHelperFactory.Create(apartmentInHouse, tariffsCrossRegionCoefficient);
						housesWithSellerInfo.FirstOrDefault(x => CanAddToObjects(x, apartment))?.ObjectsInHouse.Add(apartment);
					}

					var regionGroupId = apartmentsInHouseDb.First().ViewNMarketApartmentComparisonSellers.SellerProperties.Region.RegionGroupId;
					var urlLandingPrepaymentBooking = regionOperatorReferences
						.FirstOrDefault(x => x.RegionalOperatorsBaseProperties.RegionGroupId == regionGroupId)?.Url;

					var complexesFromHouseWithSellerInfo = housesWithSellerInfo
						.Select(house => ComplexHelperFactory.Create(house, urlLandingPrepaymentBooking, regionGroupId))
						.ToArray();

					foreach (var complex in complexesFromHouseWithSellerInfo)
					{
						if (actualComplexes.All(x => x.ComplexId != complex.ComplexId ||
						                             x.SellerId != complex.SellerId ||
						                             x.SellerType != complex.SellerType ||
						                             x.RealtyObjectType != complex.RealtyObjectType))
						{
							actualComplexes.Add(complex);

							continue;
						}

						if (actualComplexes.Any(x => x.ComplexId == complex.ComplexId &&
						                             x.SellerId == complex.SellerId &&
						                             x.SellerType == complex.SellerType &&
						                             x.RealtyObjectType == complex.RealtyObjectType &&
						                             x.HousesInComplex.All(y =>
							                             y.HouseId != complex.HousesInComplex.First().HouseId)))
						{
							actualComplexes.First(x => x.ComplexId == complex.ComplexId &&
							                           x.SellerId == complex.SellerId &&
							                           x.SellerType == complex.SellerType &&
							                           x.RealtyObjectType == complex.RealtyObjectType &&
							                           x.HousesInComplex.All(y =>
								                           y.HouseId != complex.HousesInComplex.First().HouseId))
								.HousesInComplex.Add(complex.HousesInComplex.First());
						}
					}
				}
			}

			return actualComplexes.Select(x => ComplexFactory.Create(
				x,
				operatorSellerConditions.Where(cond => cond.SellerId == x.SellerId)
					.Select(fees => fees.ConditionsOfPaymentFees).FirstOrDefault(),
				sellerCommissionPrepayments.Any(prepayment =>
					prepayment.SellerId == x.SellerId && prepayment.SellerType == x.SellerType))).ToList();
		}

		/// <summary>
		/// Получение объекта IQueryable, представляющий входную последовательность сущности <see cref="NMarketApartmentLocalEntity"/> - данных о помещениях.
		/// </summary>
		/// <returns> Объект IQueryable, представляющий входную последовательность сущности <see cref="NMarketApartmentLocalEntity"/> - данных о помещениях.</returns>
		private IQueryable<NMarketApartmentLocalEntity> GetNMarketApartmentsQueryable()
		{
			return _dbContext.NMarketApartmentLocal
				.Include(x => x.ViewNMarketApartmentCommissions)
				.Include(x => x.ViewNMarketApartmentComparisonSellers)
				.ThenInclude(x => x.SellerProperties)
				.ThenInclude(x => x.Region)
				.Include(x => x.ViewNMarketApartmentComparisonSellers)
				.Where(x => !_workerSettings.Value.IgnoreSellerIds.Contains(x.ViewNMarketApartmentComparisonSellers.SellerProperties.BuilderId))
				.AsNoTracking()
				.AsQueryable();
		}

		/// <summary>
		/// Получение объекта IQueryable, представляющий входную последовательность сущности <see cref="NMarketHouseLocalEntity"/> - данных о корпусах и комплексах.
		/// </summary>
		/// <returns> Объект IQueryable, представляющий входную последовательность сущности <see cref="NMarketHouseLocalEntity"/> - данных о корпусах и комплексах.</returns>
		private IQueryable<NMarketHouseLocalEntity> GetNMarketHousesQueryable()
		{
			return _dbContext.NMarketHouseLocal
				.OrderBy(x => x.HouseId)
				.AsNoTracking()
				.AsQueryable();
		}

		/// <summary>
		/// Асинхронная операция, возвращает данные сущности с продавцами с авансированнием.
		/// </summary>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Данные сущности с продавцами с авансированнием.</returns>
		private async Task<SellerCommissionPrepaymentsEntity[]> GetSellerCommissionPrepayments(CancellationToken cancellationToken)
		{
			return await _dbContext.SellerCommissionPrepayments.AsNoTracking().ToArrayAsync(cancellationToken);
		}

		/// <summary>
		/// Асинхронная операция, возвращает данные сущности с правилами предоставления услуг по проведениям сделок для связи оператор/продавец.
		/// </summary>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Данные сущности с правилами предоставления услуг по проведениям сделок для связи оператор/продавец.</returns>
		private async Task<OperatorSellerConditionsEntity[]> GetOperatorSellerConditions(CancellationToken cancellationToken)
		{
			return await _dbContext.OperatorSellerConditions.AsNoTracking().ToArrayAsync(cancellationToken);
		}

		/// <summary>
		/// Асинхронная операция, возвращает данные сущности коэффициентов кроссрегиональных операций.
		/// </summary>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Данные сущности коэффициентов кроссрегиональных операций.</returns>
		private async Task<TariffZoneRegionsEntity[]> GetTariffZoneRegions(CancellationToken cancellationToken)
		{
			return await _dbContext.TariffZoneRegions.AsNoTracking().ToArrayAsync(cancellationToken);
		}

		/// <summary>
		/// Асинхронная операция, возвращает данные сущности со ссылками, на информационные ресурсы региональных операторов.
		/// </summary>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Данные сущности со ссылками, на информационные ресурсы региональных операторов.</returns>
		private async Task<RegionOperatorReferenceEntity[]> GetRegionOperatorReferences(CancellationToken cancellationToken)
		{
			return await _dbContext.RegionOperatorReference
				.Include(x => x.RegionalOperatorsBaseProperties)
				.AsNoTracking()
				.ToArrayAsync(cancellationToken);
		}
	}
}