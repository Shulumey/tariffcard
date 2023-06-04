using System;

using AutoMapper;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TariffCardService.Core.Interfaces;
using TariffCardService.Core.Interfaces.Data;
using TariffCardService.DataAccess.DataProviders;
using TariffCardService.DataAccess.Interfaces;

namespace TariffCardService.DataAccess.Infrastructure
{
	/// <summary>
	/// Расширение для регистрации сервисов слоя данных.
	/// </summary>
	public static class DataAccessDependencyInjection
	{
		/// <summary>
		/// Регистрирует сервисы слоя данных.
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/>.</param>
		/// <param name="configuration"><see cref="IConfiguration"/>.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddDataAccessServices(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			var readConnectionString = configuration.GetConnectionString("PostgresDb");
			services.AddDbContext<ITariffCardDbContext, TariffCardDbContext>(builder =>
				builder.UseNpgsql(
					readConnectionString,
					sqlOptions => { sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null); }));

			services.AddAutoMapper();

			services.AddScoped<ISnapshotCatalogProvider, SnapshotCatalogProvider>();
			services.AddScoped<IComplexProvider, ComplexProvider>();
			services.AddScoped<ISearchParamAliasProvider, SearchParamAliasProvider>();

			services.AddValidatorsFromAssemblyContaining<DataAccessLayer>();

			return services;
		}

		/// <summary>
		/// Регистрирует <see cref="IMapper"/>.
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/>.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddAutoMapper(this IServiceCollection services)
		{
			services.Scan(scan => scan
				.FromAssemblyOf<DataAccessLayer>()
				.AddClasses(classes => classes.AssignableTo<Profile>())
				.As<Profile>()
				.WithSingletonLifetime());

			return services.AddSingleton(sp =>
			{
				var mapper = new MapperConfiguration(
					cfg =>
					{
						foreach (var profile in sp.GetServices<Profile>())
						{
							cfg.AddProfile(profile);
						}
					});

				mapper.AssertConfigurationIsValid();

				return mapper.CreateMapper();
			});
		}
	}
}