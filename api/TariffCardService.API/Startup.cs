using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TariffCardService.API.Infrastructure;
using TariffCardService.Business.Infrastructure;
using TariffCardService.DataAccess.Infrastructure;

namespace TariffCardService.API
{
	/// <summary>
	/// Подготавливает приложение перед запуском.
	/// </summary>
	public class Startup
	{
		/// <inheritdoc cref="IConfiguration"/>
		private readonly IConfiguration _configuration;

		/// <inheritdoc cref="IWebHostEnvironment"/>
		private readonly IWebHostEnvironment _environment;

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="Startup"/>.
		/// </summary>
		/// <param name="configuration"><see cref="IConfiguration"/>.</param>
		/// <param name="env"><see cref="IWebHostEnvironment"/>.</param>
		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			_configuration = configuration;
			_environment = env;
		}

		/// <summary>
		/// Настраивает сервис.
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/>.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			if (!_environment.IsProduction())
			{
				services.AddMiniProfiler();
			}

			// Регистрация сервисов
			services
				.AddApiVersioning(config =>
				{
					config.AssumeDefaultVersionWhenUnspecified = true;
					config.DefaultApiVersion = new ApiVersion(1, 0);
					config.ReportApiVersions = true;
					config.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
				})
				.AddVersionedApiExplorer(setup =>
				{
					setup.GroupNameFormat = "'v'VVV";
					setup.SubstituteApiVersionInUrl = true;
				})
				.AddSwaggerDocumentation()
				.AddBusinessServices()
				.AddDataAccessServices(_configuration)
				.AddCustomAuthentication(_configuration)
				.AddAuthorization();

			services
				.AddHealthChecks()
				.AddNpgSql(_configuration.GetConnectionString("PostgresDb"), name: "PostgresDb");
		}

		/// <summary>
		/// Настраивает приложение.
		/// </summary>
		/// <param name="app"><see cref="IApplicationBuilder"/>.</param>
		/// <param name="provider"><see cref="IApiVersionDescriptionProvider"/>.</param>
		public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
		{
			if (!_environment.IsProduction())
			{
				app.UseSwaggerDocumentation(provider);
				app.UseDeveloperExceptionPage();
				app.UseMiniProfiler();
			}

			app.UseCors(c =>
			{
				c.AllowAnyHeader();
				c.AllowAnyMethod();
				c.AllowAnyOrigin();
			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHealthChecks("/health");
			});
		}
	}
}