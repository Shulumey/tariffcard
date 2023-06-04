using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TariffCardService.Business.Features.Complexes.Command;
using TariffCardService.Core.Dto;
using TariffCardService.Core.Enum;

namespace TariffCardService.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с основной тарифной картой.
    /// </summary>
    [Authorize(AuthenticationSchemes = Constants.Authentication.DefaultScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("{regionGroupId:int}/[controller]")]
    public class ComplexesController : ControllerBase
    {
	    /// <inheritdoc cref="IMediator"/>
	    private readonly IMediator _mediator;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ComplexesController"/>.
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/>.</param>
	    public ComplexesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Возвращает последний корректный снимок данных.
        /// </summary>
        /// <param name="regionGroupId">Id запрашиваемой региональной группы.</param>
        /// <param name="realtyObjectType"><see cref="RealtyObjectType"/> Тип объекта недвижимости.</param>
        /// <param name="sellerType"><see cref="SellerType"/> Тип продавца.</param>
        /// <param name="searchString"><see cref="SellerType"/> Поисковая строка.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns><see cref="ComplexStatisticsWrapperDto"/> Последний корректный снимок данных.</returns>
	    [HttpGet]
	    [Route("{realtyObjectType}/{sellerType}")]
	    public Task<ComplexStatisticsWrapperDto> GetComplexes(
	        [FromRoute]
	        int regionGroupId,
	        [FromRoute]
	        RealtyObjectType realtyObjectType,
	        [FromRoute]
	        SellerType sellerType,
	        [FromQuery]
	        string searchString,
	        CancellationToken cancellationToken)
        {
	       return _mediator.Send(
		        new GetComplexes.Command
		        {
			        RegionGroupId = regionGroupId,
			        RealtyObjectType = realtyObjectType,
			        SellerType = sellerType,
			        SearchString = searchString?.Trim(),
		        }, cancellationToken);
        }

        /// <summary>
        /// Возвращает данные о группах домов.
        /// </summary>
        /// <param name="complexId">ID записи комплекса.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns><see cref="IReadOnlyCollection{HouseGroupDto}"/> Данные о группах домов.</returns>
	    [HttpGet]
	    [Route("house-groups/{complexId}/")]
	    public Task<IReadOnlyCollection<HouseGroupDto>> GetHouseGroupsAsync(long complexId, CancellationToken cancellationToken) =>
            _mediator.Send(
            	new GetHouseGroups.Command
                {
                    ComplexId = complexId,
                },
            	cancellationToken);
    }
}
