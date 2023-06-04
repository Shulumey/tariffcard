using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Quartz;

using TariffCardService.Business.Infrastructure;

using TariffCardService.DataAccess.Infrastructure;
using TariffCardService.Worker.Configurations;
using TariffCardService.Worker.Infrastructure;
using TariffCardService.Worker.Interfaces;

namespace TariffCardService.Worker
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
			services.Configure<WorkerSettings>(_configuration.GetSection(WorkerSettings.SectionName));

			services
				.AddDataAccessServices(_configuration)
				.AddBusinessServices()
				.AddSchedulerServices();

			services.AddDbContext<ITariffCardCommonDbContext, TariffCardCommonDbContext>(builder => builder.
				UseSqlServer(
					_configuration.GetConnectionString("MsSqlDb"),
					sqlOptions =>
				{
					sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
					sqlOptions.CommandTimeout(100);
				}));

			services.AddQuartz(
				q =>
				{
					q.UseMicrosoftDependencyInjectionJobFactory();
				});
			services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

			services.AddHealthChecks()
				.AddSqlServer(_configuration.GetConnectionString("MsSqlDb"), name: "MsSqlDb")
				.AddNpgSql(_configuration.GetConnectionString("PostgresDb"), name: "PostgresDb");
		}

		/// <summary>
		/// Настраивает приложение.
		/// </summary>
		/// <param name="app"><see cref="IApplicationBuilder"/>.</param>
		public void Configure(IApplicationBuilder app)
		{
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHealthChecks("/health");
			});
		}
	}
}