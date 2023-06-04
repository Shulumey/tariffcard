using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

namespace TariffCardService.Worker
{
    /// <summary>
    /// Базовый класс приложения.
    /// </summary>
    public class Program
    {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		public static Task Main(string[] args) => CreateHostBuilder(args).Build().RunAsync();

		/// <summary>
		/// Create host builder.
		/// </summary>
		/// <param name="args">Arguments.</param>
		/// <returns>Created host builder.</returns>
		private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
	            .ConfigureLogging((conf, logging) =>
                {
                    NLog.LogManager.Configuration = new NLogLoggingConfiguration(conf.Configuration.GetSection("NLog"));

                    logging
	                    .SetMinimumLevel(LogLevel.Trace)
	                    .ClearProviders()
	                    .AddNLog();
                })
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}