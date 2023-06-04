using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

using Microsoft.Extensions.DependencyInjection;

namespace TariffCardService.API.Infrastructure
{
    /// <summary>
    /// Регистрация Swagger в приложении.
    /// </summary>
    public static class Swagger
    {
	    /// <summary>
	    /// Регистрирует сервис формирования документации.
	    /// </summary>
	    /// <param name="services"><see cref="IServiceCollection"/>.</param>
	    /// <returns><see cref="IServiceCollection"/>.</returns>
	    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
	    {
		    services.AddSwaggerGen();
		    services.ConfigureOptions<ConfigureSwaggerOptions>();

		    return services;
	    }

        /// <summary>
        /// Инициализирует Swagger и SwaggerUI.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <param name="provider"><see cref="IApiVersionDescriptionProvider"/>.</param>
        /// <returns><see cref="IApplicationBuilder"/>.</returns>
	    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
	                c.SwaggerEndpoint(
		                $"./{description.GroupName}/swagger.json",
		                $"TariffCard API {description.GroupName.ToUpperInvariant()}");
                }

                c.DisplayRequestDuration();
                c.DefaultModelsExpandDepth(0);
                c.HeadContent +=
                    "<script async='async' id='mini-profiler' src='../mini-profiler-resources/includes.min.js?v=4.2.22' " +
                    "data-version='4.2.22' data-path='../mini-profiler-resources/' " +
                    "data-current-id='' " +
                    "data-scheme = 'Auto'" +
                    "data-ids='' data-position='Left' data-authorized='true' " +
                    "data-max-traces='15' data-toggle-shortcut='Alt+P' " +
                    "data-trivial-milliseconds='2.0' " +
                    "data-ignored-duplicate-execute-types='Open,OpenAsync,Close,CloseAsync'></script>\r\n";
            });

            return app;
        }
    }
}