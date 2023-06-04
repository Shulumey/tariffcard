using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;
using TariffCardService.Core.Dto;
using TariffCardService.Core.Enum;
using TariffCardService.Core.Interfaces;
using TariffCardService.Core.Interfaces.Data;
using TariffCardService.Core.Models;
using TariffCardService.DataAccess.Interfaces;

using SnapshotCatalog = TariffCardService.DataAccess.Entities.SnapshotCatalog;

namespace TariffCardService.DataAccess.DataProviders
{
	/// <inheritdoc />
	public class SnapshotCatalogProvider : ISnapshotCatalogProvider
	{
		/// <inheritdoc cref="ITariffCardDbContext"/>
		private readonly ITariffCardDbContext _dbContext;

		/// <inheritdoc cref="IMapper"/>
		private readonly IMapper _mapper;

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="SnapshotCatalogProvider"/>.
		/// </summary>
		/// <param name="dbContext"><see cref="ITariffCardDbContext"/>.</param>
		/// <param name="mapper"><see cref="IMapper"/>.</param>
		public SnapshotCatalogProvider(ITariffCardDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<ComplexDto>> GetComplexesOfSnapshotAsync(
			DateTime snapshotDate,
			int regionGroupId,
			SellerType[] sellerTypes,
			RealtyObjectType[] realtyObjectTypes,
			CancellationToken cancellationToken)
		{
			return await _dbContext.SnapshotsCatalog
				.Where(s => s.Date.Date == snapshotDate.Date)
				.Join(
					_dbContext.ComplexSnapshots,
					catalog => catalog.Id,
					complex => complex.SnapshotId,
					(catalog, complex) => complex)
				.Where(c => c.RegionGroupId == regionGroupId &&
				            sellerTypes.Contains(c.SellerType) &&
				            realtyObjectTypes.Cast<RealtyObjectType?>().Contains(c.RealtyObjectType))
				.ProjectTo<ComplexDto>(_mapper.ConfigurationProvider)
				.ToArrayAsync(cancellationToken);
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<HouseGroupDto>> GetHousesSnapshotsAsync(int complexSnapshotId, CancellationToken cancellationToken)
		{
			return await _dbContext.HouseSnapshots
				.Where(h => h.ComplexSnapshotId == complexSnapshotId)
				.Include(h => h.ObjectGroups)
				.ProjectTo<HouseGroupDto>(_mapper.ConfigurationProvider)
				.ToArrayAsync(cancellationToken);
		}

		/// <inheritdoc />
		public async Task AddSnapshotCatalogAsync(Core.Models.SnapshotCatalog snapshotCatalog, CancellationToken cancellationToken)
		{
			var snapshotCatalogEntity = _mapper.Map<SnapshotCatalog>(snapshotCatalog);
			await _dbContext.SnapshotsCatalog.AddAsync(snapshotCatalogEntity, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}