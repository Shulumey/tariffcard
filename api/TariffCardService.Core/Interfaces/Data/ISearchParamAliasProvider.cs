using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using TariffCardService.Core.Models;

namespace TariffCardService.Core.Interfaces.Data
{
	/// <summary>
	/// Провайдер для работы с поисковыми параметрами.
	/// </summary>
	public interface ISearchParamAliasProvider
	{
		/// <summary>
		/// Получает действительный список строк, по которым также будет вестись поиск.
		/// </summary>
		/// <param name="alias">Псевдоним.</param>
		/// <param name="regionalGroupId">ID региональной группы.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Найденные действительные строки для поиска.</returns>
		Task<IReadOnlyCollection<string>> GetSearchParamAliasActualValuesAsync(string alias, int regionalGroupId, CancellationToken cancellationToken);

		/// <summary>
		/// Удаление поисковые параметры.
		/// </summary>
		/// <param name="entityIds">Id сущностей, которые необходимо удалить.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns><see cref="Task"/>.</returns>
		Task RemoveAsync(int[] entityIds, CancellationToken cancellationToken);

		/// <summary>
		/// Добавление поисковые параметры.
		/// </summary>
		/// <param name="entites">Сущности, которые необходимо добавить.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns><see cref="Task"/>.</returns>
		Task AddAsync(IReadOnlyCollection<SearchParamAlias> entites, CancellationToken cancellationToken);

		/// <summary>
		/// Обновление поисковые параметры.
		/// </summary>
		/// <param name="entites">Сущности, которые необходимо обновить.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns><see cref="Task"/>.</returns>
		Task UpdateAsync(IReadOnlyCollection<SearchParamAlias> entites, CancellationToken cancellationToken);

		/// <summary>
		/// Обновление поисковые параметры.
		/// </summary>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Набор поисковых параметров.</returns>
		Task<IReadOnlyCollection<SearchParamAlias>> GetAllAsync(CancellationToken cancellationToken);
	}
}