using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using JetBrains.Annotations;

using MediatR;

using TariffCardService.Core.Interfaces;
using TariffCardService.Core.Interfaces.Data;
using TariffCardService.Core.Models;

namespace TariffCardService.Business.Features.Complexes.Command
{
	/// <summary>
	/// Получение основной тарифной карты.
	/// </summary>
	public static class CreateComplexes
	{
		/// <inheritdoc />
		public sealed class Command : IRequest
		{
			/// <summary>
			/// Список комиссионных, которые необходимо создать.
			/// </summary>
			public IReadOnlyCollection<Complex> Complexes { get; set; }
		}

		/// <summary>
		/// Валидатор для <see cref="Command"/>.
		/// </summary>
		[UsedImplicitly]
		public sealed class Validator : AbstractValidator<Command>
		{
			/// <summary>
			/// Инициализирует новый экземпляр класса <see cref="Validator"/>.
			/// </summary>
			public Validator()
			{
				RuleFor(q => q.Complexes)
					.NotEmpty()
					.WithMessage("Снимки данных тарифной карты не должны быть пустыми");
			}
		}

		/// <inheritdoc />
		public sealed class Handler : IRequestHandler<Command>
		{
			/// <inheritdoc cref="IComplexProvider"/>
			private readonly IComplexProvider _complexesProvider;

			/// <summary>
			/// Инициализирует новый экземпляр класса <see cref="Handler"/>.
			/// </summary>
			/// <param name="complexesProvider"><see cref="IComplexProvider"/>.</param>
			public Handler(IComplexProvider complexesProvider)
			{
				_complexesProvider = complexesProvider;
			}

			/// <inheritdoc />
			public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
				await _complexesProvider.AddComplexesAsync(request.Complexes, cancellationToken);

				return default;
			}
		}
	}
}