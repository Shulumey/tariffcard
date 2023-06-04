using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using TariffCardService.Core.Dto;
using TariffCardService.Core.Enum;
using TariffCardService.Core.Models;

namespace TariffCardService.Core.Interfaces.Data
{
	/// <summary>
	/// Интерфейс для работы с данными по снимкам данных.
	/// </summary>
	public interface IComplexProvider
	{
		/// <summary>
		/// Получение DTO комлекса.
		/// </summary>
		/// <param name="regionGroupId">Региональная группа.</param>
		/// <param name="objectTypes">Типы объектов.</param>
		/// <param name="sellerType">Тип продавца.</param>
		/// <param name="searchStrings">Поисковая строка.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Данные комплекса.</returns>
		Task<IReadOnlyCollection<Complex>> GetComplexesAsync(
			int regionGroupId,
			IReadOnlyCollection<RealtyObjectType> objectTypes,
			SellerType sellerType,
			IEnumerable<string> searchStrings,
			CancellationToken cancellationToken);

		/// <summary>
		/// Получение DTO группы домов.
		/// </summary>
		/// <param name="complexId">ID записи.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Данные группы домов.</returns>
		Task<IReadOnlyCollection<HouseGroupDto>> GetHouseGroupDtosAsync(long complexId, CancellationToken cancellationToken);

		/// <summary>
		/// Получение DTO группы объектов.
		/// </summary>
		/// <param name="houseGroupId">ID записи группы домов.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Данные группы объектов.</returns>
		Task<IReadOnlyCollection<ObjectGroupDto>> GetObjectGroupDtosAsync(long houseGroupId, CancellationToken cancellationToken);

		/// <summary>
		/// Добавление нового снимка данных.
		/// </summary>
		/// <param name="complex">Снимок данных, который требуется добавить.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Добавленный снимок данных.</returns>
		Task AddComplexesAsync(IReadOnlyCollection<Complex> complex, CancellationToken cancellationToken);

		/// <summary>
		/// Обновление данных хранимого снимка данных.
		/// </summary>
		/// <param name="currentComplexes">Снимок данных, который требуется добавить.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Добавленный снимок данных.</returns>
		Task UpdateComplexesAsync(IReadOnlyCollection<Complex> currentComplexes, CancellationToken cancellationToken);
	}
}