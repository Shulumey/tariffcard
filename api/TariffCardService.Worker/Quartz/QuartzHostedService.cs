using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

using Quartz;

namespace TariffCardService.Worker.Quartz
{
	/// <inheritdoc />
	public class QuartzHostedService : IHostedService
	{
		/// <inheritdoc cref="IScheduler"/>
		private readonly IScheduler _scheduler;

		/// <summary>
		/// Инициализирует экземпляр <see cref="QuartzHostedService"/>.
		/// </summary>
		/// <param name="scheduler">Фабрика расписаний задач.</param>
		public QuartzHostedService(
			IScheduler scheduler)
		{
			_scheduler = scheduler;
		}

		/// <inheritdoc />
		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _scheduler.Start(cancellationToken);
		}

		/// <inheritdoc />
		public Task StopAsync(CancellationToken cancellationToken)
		{
			return _scheduler.Shutdown(true, cancellationToken);
		}
	}
}
