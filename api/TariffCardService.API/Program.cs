using System.Diagnostics;
using System.Reflection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

namespace TariffCardService.API
{
	/// <summary>
	/// Базовый класс приложения.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Точка входа в приложение.
		/// </summary>
		/// <param name="args">Аргументы запуска приложения.</param>
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		/// <summary>
		/// Инициализация приложения.
		/// </summary>
		/// <param name="args">Аргументы запуска приложения.</param>
		/// <returns><see cref="IHostBuilder"/>.</returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureLogging((ctx, logging) =>
				{
					NLog.LogManager.Configuration = new NLogLoggingConfiguration(ctx.Configuration.GetSection("NLog"));

					logging
						.SetMinimumLevel(LogLevel.Trace)
						.ClearProviders()
						.AddNLog();
				})
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
	}
}