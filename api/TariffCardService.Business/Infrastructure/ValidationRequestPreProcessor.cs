using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using JetBrains.Annotations;

using MediatR.Pipeline;

namespace TariffCardService.Business.Infrastructure
{
	/// <summary>
	/// Проверяет запрос перед выполнением.
	/// </summary>
	/// <typeparam name="TRequest">Тип запроса.</typeparam>
	[UsedImplicitly]
	public sealed class ValidationRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
	{
		/// <inheritdoc cref="IValidator"/>
		private readonly IValidator<TRequest> _validator;

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ValidationRequestPreProcessor{TRequest}"/>.
		/// </summary>
		/// <param name="validator"><see cref="IValidator"/>.</param>
		public ValidationRequestPreProcessor(IValidator<TRequest> validator = null)
		{
			_validator = validator;
		}

		/// <inheritdoc />
		public async Task Process(TRequest request, CancellationToken cancellationToken)
		{
			if (_validator == null)
				return;

			await _validator.ValidateAsync(
				request,
				option => option.ThrowOnFailures(),
				cancellationToken);
		}
	}
}