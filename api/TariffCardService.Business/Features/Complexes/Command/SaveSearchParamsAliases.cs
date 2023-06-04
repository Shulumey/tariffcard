using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using TariffCardService.Core.Interfaces;
using TariffCardService.Core.Interfaces.Data;
using TariffCardService.Core.Models;

namespace TariffCardService.Business.Features.Complexes.Command
{
	/// <summary>
	/// Синхронизация поисковых псевдонимов.
	/// </summary>
	public static class SaveSearchParamsAliases
	{
		/// <inheritdoc />
		public sealed class Command : IRequest
		{
			/// <summary>
			/// Коллекция поисковых псевдонимов.
			/// </summary>
			public IReadOnlyCollection<SearchParamAlias> SearchParamAliases { get; set; }
		}

		/// <inheritdoc />
		public sealed class Handler : IRequestHandler<Command>
		{
			/// <inheritdoc cref="ISearchParamAliasProvider"/>
			private readonly ISearchParamAliasProvider _searchParamsStringProvider;

			/// <summary>
			/// Инициализирует новый экземпляр класса <see cref="Handler"/>.
			/// </summary>
			/// <param name="searchParamsStringProvider"><see cref="ISearchParamAliasProvider"/>.</param>
			public Handler(ISearchParamAliasProvider searchParamsStringProvider)
			{
				_searchParamsStringProvider = searchParamsStringProvider;
			}

			/// <inheritdoc />
			public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
				IReadOnlyCollection<SearchParamAlias> searchParamAliases = await _searchParamsStringProvider.GetAllAsync(cancellationToken);

				int[] targetIds = searchParamAliases.Select(x => x.Id).ToArray();
				int[] sourceIds = request.SearchParamAliases.Select(x => x.Id).ToArray();

				int[] mustToRemoveIds = targetIds.Except(sourceIds).ToArray();
				int[] mustToAddIds = sourceIds.Except(targetIds).ToArray();
				int[] mustToUpdateIds = sourceIds.Intersect(targetIds).ToArray();

				await _searchParamsStringProvider.RemoveAsync(mustToRemoveIds, cancellationToken);
				await _searchParamsStringProvider.AddAsync(request.SearchParamAliases.Where(x => mustToAddIds.Contains(x.Id)).ToArray(), cancellationToken);
				await _searchParamsStringProvider.UpdateAsync(request.SearchParamAliases.Where(x => mustToUpdateIds.Contains(x.Id)).ToArray(), cancellationToken);

				return default;
			}
		}
	}
}