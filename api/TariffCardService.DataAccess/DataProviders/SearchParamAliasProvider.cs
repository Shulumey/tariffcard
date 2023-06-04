using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TariffCardService.Core.Interfaces.Data;
using TariffCardService.Core.Models;
using TariffCardService.DataAccess.Interfaces;

namespace TariffCardService.DataAccess.DataProviders
{
	/// <inheritdoc />
	public class SearchParamAliasProvider : ISearchParamAliasProvider
	{
		/// <inheritdoc cref="ITariffCardDbContext"/>>
		private readonly ITariffCardDbContext _dbContext;

		/// <inheritdoc cref="IMapper"/>>
		private readonly IMapper _mapper;

		/// <summary>
		/// Initialized provider.
		/// </summary>
		/// <param name="dbContext"><see cref="ITariffCardDbContext"/>.</param>
		/// <param name="mapper"><see cref="IMapper"/>.</param>
		public SearchParamAliasProvider(ITariffCardDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		/// <inheritdoc/>
		public async Task<IReadOnlyCollection<string>> GetSearchParamAliasActualValuesAsync(string alias, int regionalGroupId, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(alias))
			{
				return await Task.FromResult(Enumerable.Empty<string>().ToArray());
			}

			return await _dbContext.SearchParamAliases
				.Where(x => x.RegionalGroupId == regionalGroupId && x.Alias.ToUpper().Contains(alias.Trim().ToUpper()))
				.Select(x => x.Value)
				.Distinct()
				.ToArrayAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public async Task RemoveAsync(int[] entityIds, CancellationToken cancellationToken)
		{
			if (entityIds.Any())
			{
				IEnumerable<Entities.SearchParamAlias> paramsMustRemove = _dbContext.SearchParamAliases
					.Where(x => entityIds.Contains(x.Id))
					.ToArray();

				_dbContext.SearchParamAliases.RemoveRange(paramsMustRemove);

				await _dbContext.SaveChangesAsync(cancellationToken);
			}
		}

		/// <inheritdoc/>
		public async Task AddAsync(IReadOnlyCollection<SearchParamAlias> entites, CancellationToken cancellationToken)
		{
			if (entites.Any())
			{
				_dbContext.SearchParamAliases.AddRange(_mapper.Map<List<Entities.SearchParamAlias>>(entites));

				await _dbContext.SaveChangesAsync(cancellationToken);
			}
		}

		/// <inheritdoc/>
		public async Task UpdateAsync(IReadOnlyCollection<SearchParamAlias> entites, CancellationToken cancellationToken)
		{
			if (entites.Any())
			{
				IReadOnlyCollection<Entities.SearchParamAlias> entitiesMustUpdate = _mapper.Map<IReadOnlyCollection<Entities.SearchParamAlias>>(entites);

				foreach (var searchParamAlias in entitiesMustUpdate)
				{
					_dbContext.SearchParamAliases.Attach(searchParamAlias);
					_dbContext.Entry(searchParamAlias).State = EntityState.Modified;
				}

				await _dbContext.SaveChangesAsync(cancellationToken);
			}
		}

		/// <inheritdoc/>
		public async Task<IReadOnlyCollection<SearchParamAlias>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await _dbContext.SearchParamAliases
				.ProjectTo<SearchParamAlias>(_mapper.ConfigurationProvider)
				.ToArrayAsync(cancellationToken);
		}
	}
}