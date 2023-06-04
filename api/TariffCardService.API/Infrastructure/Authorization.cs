using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NmarketAuthLibrary.Extensions;
using NmarketAuthLibrary.Options;

namespace TariffCardService.API.Infrastructure
{
	/// <summary>
	/// Регистрация авторизации в приложении.
	/// </summary>
	public static class Authorization
	{
		/// <summary>
		/// Добавление аутентификации.
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/>.</param>
		/// <param name="configuration"><see cref="IConfiguration"/>.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddNmarketAuthorization(new NmarketAuthorizationSettings
			{
				Authority = configuration["AuthServerUrl"],
			});

			return services;
		}
	}
}