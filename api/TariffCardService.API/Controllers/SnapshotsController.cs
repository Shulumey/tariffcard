using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using TariffCardService.Business.Features.Snapshots.Command;
using TariffCardService.Core.Dto;
using TariffCardService.Core.Enum;

namespace TariffCardService.API.Controllers
{
	/// <summary>
	/// Контролер для работы со снимками данных тарифной карты.
	/// </summary>
	[ApiController]
	[ApiVersion("1.0")]
	[Route("{regionGroupId:int}/[controller]")]
	public class SnapshotsController : ControllerBase
	{
		/// <inheritdoc cref="IMediator"/>
		private readonly IMediator _mediator;

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="SnapshotsController"/>.
		/// </summary>
		/// <param name="mediator"><see cref="IMediator"/>.</param>
		public SnapshotsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Получение коммиссий на дату для ЖК.
		/// </summary>
		/// <param name="regionGroupId">ID региональной группы.</param>
		/// <param name="date">Дата ТК.</param>
		/// <param name="sellerType">Тип продавца.</param>
		/// <param name="realtyObjectType">Тип объекта недвижимости.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns><see cref="IReadOnlyCollection{ComplexDto}"/>Коллекция снимков данных на определенную дату.</returns>
		[HttpGet]
		[Route("{date}/{sellerType}/{realtyObjectType}")]
		public Task<IReadOnlyCollection<ComplexDto>> GetComplexes(
			int regionGroupId,
			DateTime date,
			SellerType sellerType,
			RealtyObjectType realtyObjectType,
			CancellationToken cancellationToken) =>
			_mediator.Send(new GetComplexesSnapshot.Command(date, regionGroupId, new[] { sellerType }, new[] { realtyObjectType }), cancellationToken);

		/// <summary>
		/// Получение корпусов по ЖК.
		/// </summary>
		/// <param name="complexSnapshotId">ID записи комплекса.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		/// <returns>Коллекция снимков данных по корпусам.</returns>
		[HttpGet]
		[Route("{complexSnapshotId}")]
		public Task<IReadOnlyCollection<HouseGroupDto>> GetHouses(int complexSnapshotId, CancellationToken cancellationToken) =>
			_mediator.Send(new GetHousesSnapshot.Command(complexSnapshotId), cancellationToken);
	}
}