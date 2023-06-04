using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using TariffCardService.Core.Dto;
using TariffCardService.Core.Enum;
using TariffCardService.Core.Interfaces;
using TariffCardService.Core.Interfaces.Data;

namespace TariffCardService.Business.Features.Snapshots.Command
{
	/// <summary>
	/// Получение снимка данных ЖК на определенную дату.
	/// </summary>
	public class GetComplexesSnapshot
	{
		/// <inheritdoc/>
		public sealed class Command : IRequest<IReadOnlyCollection<ComplexDto>>
		{
			/// <summary>
			/// Инициализирует новый экземпляр класса <see cref="GetComplexesSnapshot"/>.
			/// </summary>
			/// <param name="snapshotDate">дата снимка данных.</param>
			/// <param name="regionGroupId">ID региональной группы.</param>
			/// <param name="sellerTypes">Типы продавцов.</param>
			/// <param name="objectTypes">Типы объектов недвижимости.</param>
			public Command(DateTime snapshotDate, int regionGroupId, SellerType[] sellerTypes, RealtyObjectType[] objectTypes)
			{
				SnapshotDate = snapshotDate;
				RegionGroupId = regionGroupId;
				SellerTypes = sellerTypes;
				RealtyObjectTypes = objectTypes;
			}

			/// <summary>
			/// дата снимка данных.
			/// </summary>
			public DateTime SnapshotDate { get; }

			/// <summary>
			/// ID региональной группы.
			/// </summary>
			public int RegionGroupId { get; }

			/// <summary>
			/// Типы продавцов.
			/// </summary>
			public SellerType[] SellerTypes { get; }

			/// <summary>
			/// Типы объектов недвижимости.
			/// </summary>
			public RealtyObjectType[] RealtyObjectTypes { get; }
		}

		/// <inheritdoc />
		public sealed class Handler : IRequestHandler<Command, IReadOnlyCollection<ComplexDto>>
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
			public Task<IReadOnlyCollection<ComplexDto>> Handle(Command query, CancellationToken cancellationToken) =>
				_snapshotCatalogProvider.GetComplexesOfSnapshotAsync(query.SnapshotDate, query.RegionGroupId, query.SellerTypes, query.RealtyObjectTypes, cancellationToken);
		}
	}
}