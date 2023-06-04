using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Quartz;

namespace TariffCardService.Worker.Quartz
{
	/// <inheritdoc />
	public sealed class JobWrapper : IJob
	{
		/// <inheritdoc cref="IServiceProvider" />
		private readonly IServiceProvider _serviceProvider;

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="JobWrapper"/>.
		/// </summary>
		/// <param name="serviceProvider">Контейнер зависимостей.</param>
		public JobWrapper(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		/// <inheritdoc />
		public async Task Execute(IJobExecutionContext context)
		{
			using var scope = _serviceProvider.CreateScope();

			var job = (IJob)scope.ServiceProvider.GetService(context.JobDetail.JobType);
			await job.Execute(context);
		}
	}
}
