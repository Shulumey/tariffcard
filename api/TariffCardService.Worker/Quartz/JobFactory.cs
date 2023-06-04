using System;

using Quartz;
using Quartz.Spi;

namespace TariffCardService.Worker.Quartz
{
	/// <inheritdoc />
	public class JobFactory : IJobFactory
	{
		/// <inheritdoc cref="IServiceProvider"/>
		private readonly IServiceProvider _serviceProvider;

		/// <summary>
		/// Инициализирует экземпляр <see cref="JobFactory"/>.
		/// </summary>
		/// <param name="serviceProvider"><see cref="IServiceProvider"/>.</param>
		public JobFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		/// <inheritdoc />
		public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
		{
			return new JobWrapper(_serviceProvider);
		}

		/// <inheritdoc />
		public void ReturnJob(IJob job)
		{
		}
	}
}
