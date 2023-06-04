using System;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace TariffCardService.API.Infrastructure
{
	/// <inheritdoc />
	public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
	{
		/// <inheritdoc cref="IApiVersionDescriptionProvider"/>
		private readonly IApiVersionDescriptionProvider _provider;

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="ConfigureSwaggerOptions"/>.
		/// </summary>
		/// <param name="provider"><see cref="IApiVersionDescriptionProvider"/>.</param>
		public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
		{
			_provider = provider;
		}

		/// <inheritdoc />
		public void Configure(string name, SwaggerGenOptions options) => Configure(options);

		/// <inheritdoc />
		public void Configure(SwaggerGenOptions options)
		{
			options.SchemaGeneratorOptions.SchemaIdSelector = type => type.FullName;

			foreach (var description in _provider.ApiVersionDescriptions)
			{
				options.SwaggerDoc(
					description.GroupName,
					new OpenApiInfo()
					{
						Title = "TariffCard  API",
						Version = description.ApiVersion.ToString(),
						Description = "Сервис для работы c тарифной картой",
					});
			}

			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
		}
	}
}