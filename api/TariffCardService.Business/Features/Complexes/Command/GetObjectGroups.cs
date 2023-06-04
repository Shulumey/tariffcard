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
    /// Получение данных по группам объектов.
    /// </summary>
    public class GetObjectGroups
    {
        /// <inheritdoc />
        public sealed class Command : IRequest<IReadOnlyCollection<ObjectGroupDto>>
        {
            /// <summary>
            /// ID группы домов.
            /// </summary>
            public long HouseGroupId { get; set; }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Command, IReadOnlyCollection<ObjectGroupDto>>
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
	        public Task<IReadOnlyCollection<ObjectGroupDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                return _complexProvider.GetObjectGroupDtosAsync(request.HouseGroupId, cancellationToken);
            }
        }
    }
}
