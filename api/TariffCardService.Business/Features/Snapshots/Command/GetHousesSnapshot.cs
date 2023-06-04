using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using TariffCardService.Core.Dto;
using TariffCardService.Core.Interfaces;
using TariffCardService.Core.Interfaces.Data;

namespace TariffCardService.Business.Features.Snapshots.Command
{
	/// <summary>
	/// Запрос на получение корпусов.
	/// </summary>
	public class GetHousesSnapshot
	{
		/// <inheritdoc />
		public sealed class Command : IRequest<IReadOnlyCollection<HouseGroupDto>>
		{
			/// <summary>
			/// Инициализирует новый экземпляр класса <see cref="Command"/>.
			/// </summary>
			/// <param name="complexSnapshotId">ID записи комплекса.</param>
			public Command(int complexSnapshotId)
			{
				ComplexSnapshotId = complexSnapshotId;
			}

			/// <summary>
			/// ID записи комплекса.
			/// </summary>
			public int ComplexSnapshotId { get; }
		}

		/// <inheritdoc />
		public class Handler : IRequestHandler<Command, IReadOnlyCollection<HouseGroupDto>>
		{
			/// <inheritdoc cref="ISnapshotCatalogProvider"/>
			private readonly ISnapshotCatalogProvider _snapshotCatalogProvider;

			/// <summary>
			/// Инициализирует новый экземпляр класса <see cref="Handler"/>.
			/// </summary>
			/// <param name="snapshotCatalogProvider"><see cref="ISnapshotCatalogProvider"/>.</param>
			public Handler(ISnapshotCatalogProvider snapshotCatalogProvider)
			{
				_snapshotCatalogProvider = snapshotCatalogProvider;
			}

			/// <inheritdoc />
			public Task<IReadOnlyCollection<HouseGroupDto>> Handle(Command request, CancellationToken cancellationToken) =>
				_snapshotCatalogProvider.GetHousesSnapshotsAsync(request.ComplexSnapshotId, cancellationToken);
		}
	}
}