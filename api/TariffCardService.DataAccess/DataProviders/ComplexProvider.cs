using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

using TariffCardService.Core;
using TariffCardService.Core.Dto;
using TariffCardService.Core.Enum;
using TariffCardService.Core.Interfaces.Data;
using TariffCardService.Core.Models;

using TariffCardService.DataAccess.Entities;
using TariffCardService.DataAccess.Factory;
using TariffCardService.DataAccess.Interfaces;

namespace TariffCardService.DataAccess.DataProviders
{
	/// <inheritdoc />
	public class ComplexProvider : IComplexProvider
	{
		/// <inheritdoc cref="ITariffCardDbContext"/>
		private readonly ITariffCardDbContext _dbContext;

		/// <inheritdoc cref="IMapper"/>
		private readonly IMapper _mapper;

		/// <summary>
		/// Initialized provider.
		/// </summary>
		/// <param name="dbContext"><see cref="ITariffCardDbContext"/>.</param>
		/// <param name="mapper"><see cref="IMapper"/>.</param>
		public ComplexProvider(ITariffCardDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<Complex>> GetComplexesAsync(
			int regionGroupId,
			IReadOnlyCollection<RealtyObjectType> objectTypes,
			SellerType sellerType,
			IEnumerable<string> searchStrings,
			CancellationToken cancellationToken)
		{
			return await _dbContext.CommissionComplexes
				.Where(cc => cc.RegionGroupId == regionGroupId && objectTypes.Contains(cc.RealtyObjectType) && cc.SellerType == sellerType)
				.Where(CommissionComplexConditionFactory.Create(searchStrings))
				.ProjectTo<Complex>(_mapper.ConfigurationProvider)
				.ToArrayAsync(cancellationToken);
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<HouseGroupDto>> GetHouseGroupDtosAsync(long complexId, CancellationToken cancellationToken)
        {
			var houseGroups = await _dbContext.CommissionHouseGroups
                .Where(x => x.ComplexId == complexId)
                .ToArrayAsync(cancellationToken);

			foreach (var houseGroup in houseGroups)
			{
				await _dbContext.Entry(houseGroup)
					.Collection(x => x.ObjectGroups)
					.LoadAsync(cancellationToken);
			}

			return houseGroups.Select(x => _mapper.Map<HouseGroupDto>(x)).ToArray();
        }

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<ObjectGroupDto>> GetObjectGroupDtosAsync(long houseGroupId, CancellationToken cancellationToken)
        {
			return await _dbContext.CommissionObjects
                .Where(x => x.HouseGroupId == houseGroupId)
                .Select(x => _mapper.Map<ObjectGroupDto>(x))
                .ToArrayAsync(cancellationToken);
        }

		/// <inheritdoc />
		public async Task AddComplexesAsync(IReadOnlyCollection<Complex> complexes, CancellationToken cancellationToken)
		{
			_dbContext.CommissionComplexes.AddRange(_mapper.Map<List<CommissionComplex>>(complexes));

			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		/// <inheritdoc />
		public async Task UpdateComplexesAsync(IReadOnlyCollection<Complex> currentComplexes, CancellationToken cancellationToken)
		{
			var currentSnapshotCatalogDb = _dbContext.CommissionComplexes
				.Include(x => x.HouseGroups)
				.ThenInclude(x => x.ObjectGroups)
				.AsAsyncEnumerable()
				.GetAsyncEnumerator(cancellationToken);
			var removedComplexes = new List<CommissionComplex>();
			var createdComplex = new List<CommissionComplex>();

			while (await currentSnapshotCatalogDb.MoveNextAsync())
			{
				var complexDb = currentSnapshotCatalogDb.Current;

				if (!currentComplexes.Any(snap => snap.ComplexId == complexDb.ComplexId &&
												  snap.RealtyObjectType == complexDb.RealtyObjectType &&
												  snap.RegionGroupId == complexDb.RegionGroupId &&
												  snap.SellerId == complexDb.SellerId &&
												  snap.SellerType == complexDb.SellerType))
				{
					removedComplexes.Add(complexDb);

					continue;
				}

				createdComplex.Add(complexDb);

				var complex = currentComplexes.FirstOrDefault(snap => snap.ComplexId == complexDb.ComplexId &&
																	  snap.RealtyObjectType == complexDb.RealtyObjectType &&
																	  snap.RegionGroupId == complexDb.RegionGroupId &&
																	  snap.SellerId == complexDb.SellerId &&
																	  snap.SellerType == complexDb.SellerType);

				CommissionComplexUpdate(complexDb, complex);

				if (complex == null) continue;

				HouseGroupActualization(complex.HouseGroups, complexDb.HouseGroups);
			}

			_dbContext.CommissionComplexes.RemoveRange(_mapper.Map<List<CommissionComplex>>(removedComplexes));

			var creatingComplex = currentComplexes
				.Where(currentComplex =>
					!createdComplex
						.Any(y =>
							y.ComplexId == currentComplex.ComplexId &&
							y.SellerId == currentComplex.SellerId &&
							y.SellerType == currentComplex.SellerType &&
							y.RealtyObjectType == currentComplex.RealtyObjectType))
				.ToArray();

			_dbContext.CommissionComplexes.AddRange(_mapper.Map<List<CommissionComplex>>(creatingComplex));

			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		/// <summary>
		/// Актуализирует информацию по группам домов.
		/// </summary>
		/// <param name="houseGroups">Collection <see cref="HouseGroup"/>.</param>
		/// <param name="commissionHouseGroups">Collection <see cref="CommissionHouseGroup"/>.</param>
		private void HouseGroupActualization(ICollection<HouseGroup> houseGroups, ICollection<CommissionHouseGroup> commissionHouseGroups)
		{
			foreach (var deletedHouses in commissionHouseGroups.Where(housesDb => houseGroups.All(h =>
						 h.HouseId != housesDb.HouseId ||
						 h.HouseName != housesDb.HouseName ||
						 h.RealtyObjectType != housesDb.RealtyObjectType)).ToArray())
			{
				commissionHouseGroups.Remove(deletedHouses);
			}

			foreach (var housesDb in commissionHouseGroups)
			{
				var house = houseGroups.FirstOrDefault(h => h.HouseId == housesDb.HouseId &&
															h.RealtyObjectType == housesDb.RealtyObjectType &&
															h.HouseName == housesDb.HouseName);

				if (house == null) continue;

				CommissionHouseGroupUpdate(housesDb, house);
				ObjectGroupActualization(house.ObjectGroups, housesDb.ObjectGroups);
			}

			foreach (var housesInner in houseGroups.Where(housesInner =>
						 commissionHouseGroups.All(h =>
							 h.HouseId != housesInner.HouseId ||
							 h.RealtyObjectType != housesInner.RealtyObjectType ||
							 h.HouseName != housesInner.HouseName)))
			{
				commissionHouseGroups.Add(_mapper.Map<CommissionHouseGroup>(housesInner));
			}
		}

		/// <summary>
		/// Актуализирует информацию по группам объектов недвижимости.
		/// </summary>
		/// <param name="objectGroups">Collection <see cref="ObjectGroup"/>.</param>
		/// <param name="commissionObjectGroups">Collection <see cref="CommissionObjectGroup"/>.</param>
		private void ObjectGroupActualization(ICollection<ObjectGroup> objectGroups, ICollection<CommissionObjectGroup> commissionObjectGroups)
		{
			// Определение групп объектов подлежащих удалению и удаление.
			foreach (var objectGroup in commissionObjectGroups.Where(objectInHouseDb =>
						 objectGroups.All(o =>
							 o.ApartmentId != objectInHouseDb.ApartmentId ||
							 o.RealtyObjectType != objectInHouseDb.RealtyObjectType ||
							 o.Rooms != objectInHouseDb.Rooms ||
							 o.ApartmentDescription != objectInHouseDb.ApartmentDescription)).ToArray())
			{
				commissionObjectGroups.Remove(objectGroup);
			}

			// Обновление групп объектов.
			foreach (var objectInHouseDb in commissionObjectGroups.Where(objectInHouseDb =>
						 objectGroups.Any(o =>
							 o.ApartmentId == objectInHouseDb.ApartmentId &&
							 o.RealtyObjectType == objectInHouseDb.RealtyObjectType &&
							 o.Rooms == objectInHouseDb.Rooms &&
							 o.ApartmentDescription == objectInHouseDb.ApartmentDescription)))
			{
				var objectInHouse = objectGroups.First(o =>
					o.ApartmentId == objectInHouseDb.ApartmentId &&
					o.RealtyObjectType == objectInHouseDb.RealtyObjectType &&
					o.Rooms == objectInHouseDb.Rooms &&
					o.ApartmentDescription == objectInHouseDb.ApartmentDescription);

				CommissionObjectUpdate(objectInHouseDb, objectInHouse);
			}

			// Определение новых групп объектов и добавление.
			foreach (var objectsInner in objectGroups.Where(objectsInner => commissionObjectGroups.All(o =>
						 o.ApartmentId != objectsInner.ApartmentId ||
						 o.RealtyObjectType != objectsInner.RealtyObjectType ||
						 o.Rooms != objectsInner.Rooms ||
						 o.ApartmentDescription != objectsInner.ApartmentDescription)))
			{
				commissionObjectGroups.Add(_mapper.Map<CommissionObjectGroup>(objectsInner));
			}
		}

		/// <summary>
		/// Сравнивает и актуализирует целевой объект <see cref="CommissionComplex"/> с <see cref="Complex"/>.
		/// </summary>
		/// <param name="target">Целевой объект обновления. <see cref="CommissionComplex"/>.</param>
		/// <param name="comparable">Эталонный объект сравнения. <see cref="Complex"/>.</param>
		private void CommissionComplexUpdate(CommissionComplex target, Complex comparable)
		{
			target.HousesCount = target.HousesCount.ObjectEqualsAndUpdate(comparable.HousesCount);
			target.CrossRegionAdvancedBookingCoefficient = comparable.CrossRegionAdvancedBookingCoefficient;
			target.ComplexName = target.ComplexName.ObjectEqualsAndUpdateString(comparable.ComplexName);
			target.SellerName = target.SellerName.ObjectEqualsAndUpdateString(comparable.SellerName);
			target.CommissionType = target.CommissionType.ObjectEqualsAndUpdateNullableType(comparable.CommissionType);
			target.MaxCommissionValue = target.MaxCommissionValue.ObjectEqualsAndUpdateNullableType(comparable.MaxCommissionValue);
			target.MinCommissionValue = target.MinCommissionValue.ObjectEqualsAndUpdateNullableType(comparable.MinCommissionValue);
			target.IsAdvancedBooking = comparable.IsAdvancedBooking;
			target.IsSellerCommissionPrepayments = target.IsSellerCommissionPrepayments.ObjectEqualsAndUpdate(comparable.IsSellerCommissionPrepayments);
			target.IsCommissionCalculatedFromTotalPrice = comparable.IsCommissionCalculatedFromTotalPrice;
			target.ConditionsOfPaymentFees = target.ConditionsOfPaymentFees.ObjectEqualsAndUpdateString(comparable.ConditionsOfPaymentFees);
			target.UrlLandingPrepaymentBooking = target.UrlLandingPrepaymentBooking.ObjectEqualsAndUpdateString(comparable.UrlLandingPrepaymentBooking);
		}

		/// <summary>
		/// Сравнивает и актуализирует целевой объект <see cref="CommissionHouseGroup"/> с <see cref="HouseGroup"/>.
		/// </summary>
		/// <param name="target">Целевой объект обновления. <see cref="CommissionHouseGroup"/>.</param>
		/// <param name="comparable">Эталонный объект сравнения. <see cref="HouseGroup"/>.</param>
		private void CommissionHouseGroupUpdate(CommissionHouseGroup target, HouseGroup comparable)
		{
			target.CrossRegionAdvancedBookingCoefficient = target.CrossRegionAdvancedBookingCoefficient.ObjectEqualsAndUpdate(comparable.CrossRegionAdvancedBookingCoefficient);
			target.ObjectsCount = target.ObjectsCount.ObjectEqualsAndUpdate(comparable.ObjectsCount);
			target.HasOverriding = target.HasOverriding.ObjectEqualsAndUpdate(comparable.HasOverriding);
			target.CommissionType = target.CommissionType.ObjectEqualsAndUpdateNullableType(comparable.CommissionType);
			target.CommissionValue = target.CommissionValue.ObjectEqualsAndUpdateNullableType(comparable.CommissionValue);
			target.MaxCommissionValue = target.MaxCommissionValue.ObjectEqualsAndUpdateNullableType(comparable.MaxCommissionValue);
			target.MinCommissionValue = target.MinCommissionValue.ObjectEqualsAndUpdateNullableType(comparable.MinCommissionValue);
			target.MinMaxCommissionType = target.MinMaxCommissionType.ObjectEqualsAndUpdateNullableType(comparable.MinMaxCommissionType);
		}

		/// <summary>
		/// Сравнивает и актуализирует целевой объект <see cref="CommissionObjectGroup"/> с <see cref="ObjectGroup"/>.
		/// </summary>
		/// <param name="target">Целевой объект обновления. <see cref="CommissionObjectGroup"/>.</param>
		/// <param name="comparable">Эталонный объект сравнения. <see cref="ObjectGroup"/>.</param>
		private void CommissionObjectUpdate(CommissionObjectGroup target, ObjectGroup comparable)
		{
			target.CommissionType = target.CommissionType.ObjectEqualsAndUpdate(comparable.CommissionType);
			target.CommissionValue = target.CommissionValue.ObjectEqualsAndUpdate(comparable.CommissionValue);
			target.IsOverriding = target.IsOverriding.ObjectEqualsAndUpdate(comparable.IsOverriding);
			target.CrossRegionAdvancedBookingCoefficient = target.CrossRegionAdvancedBookingCoefficient.ObjectEqualsAndUpdate(comparable.CrossRegionAdvancedBookingCoefficient);
		}
	}
}