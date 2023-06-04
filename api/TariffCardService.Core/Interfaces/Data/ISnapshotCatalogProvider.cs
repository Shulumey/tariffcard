using System;
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
	public interface ISnapshotCatalogProvider
	{
		/// <summary>
		/// Получение списка всех снимков данных.
		/// </summary>
		/// <param name="snapshotDate">дата снимка данных.</param>
		/// <param name="regionGroupId">ID региональной группы.</param>
		/// <param name="sellerTypes">Типы продавцов.</param>
		/// <param name="realtyObjectTypes">Типы объектов недвижимости.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns><see cref="IReadOnlyCollection{ComplexDto}"/>комплексы из снимков данных.</returns>
		Task<IReadOnlyCollection<ComplexDto>> GetComplexesOfSnapshotAsync(
			DateTime snapshotDate,
			int regionGroupId,
			SellerType[] sellerTypes,
			RealtyObjectType[] realtyObjectTypes,
			CancellationToken cancellationToken);

		/// <summary>
		/// Получение списка снимка данных домов.
		/// </summary>
		/// <param name="complexSnapshotId">ID записи комплекса.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Коллекция домов по снимкам данных.</returns>
		Task<IReadOnlyCollection<HouseGroupDto>> GetHousesSnapshotsAsync(int complexSnapshotId, CancellationToken cancellationToken);

		/// <summary>
		/// Добавление нового снимка данных.
		/// </summary>
		/// <param name="snapshotCatalog">Снимок данных, который требуется добавить.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Async result.</returns>
		Task AddSnapshotCatalogAsync(SnapshotCatalog snapshotCatalog, CancellationToken cancellationToken);
	}
}