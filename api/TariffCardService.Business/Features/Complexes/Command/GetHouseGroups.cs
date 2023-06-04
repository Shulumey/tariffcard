using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using TariffCardService.Core.Dto;
using TariffCardService.Core.Interfaces;
using TariffCardService.Core.Interfaces.Data;

namespace TariffCardService.Business.Features.Complexes.Command
{
    /// <summary>
    /// Получение данных по группам домов.
    /// </summary>
    public static class GetHouseGroups
    {
        /// <inheritdoc />
        public sealed class Command : IRequest<IReadOnlyCollection<HouseGroupDto>>
        {
            /// <summary>
            /// ID записи.
            /// </summary>
            public long ComplexId { get; set; }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Command, IReadOnlyCollection<HouseGroupDto>>
        {
	        /// <inheritdoc cref="IComplexProvider"/>
	        private readonly IComplexProvider _complexProvider;

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="Handler"/>.
            /// </summary>
            /// <param name="complexProvider"><see cref="IComplexProvider"/>.</param>
	        public Handler(IComplexProvider complexProvider)
            {
                _complexProvider = complexProvider;
            }

            /// <inheritdoc />
	        public Task<IReadOnlyCollection<HouseGroupDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                return _complexProvider.GetHouseGroupDtosAsync(request.ComplexId, cancellationToken);
            }
        }
    }
}
