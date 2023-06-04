using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace TariffCardService.Business.Infrastructure
{
	/// <summary>
	/// Расширение для регистрации сервисов бизнес-слоя.
	/// </summary>
	public static class BusinessDependencyInjection
	{
		/// <summary>
		/// Регистрирует сервисы бизнес-слоя.
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/>.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddBusinessServices(this IServiceCollection services)
		{
			services.AddMediatR(typeof(BusinessLayer));
			services.AddValidatorsFromAssemblyContaining<BusinessLayer>();

			return services;
		}
	}
}