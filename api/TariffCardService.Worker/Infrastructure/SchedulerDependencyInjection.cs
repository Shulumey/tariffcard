using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Quartz;
using Quartz.Impl;
using Quartz.Logging;

using TariffCardService.Worker.Jobs;
using TariffCardService.Worker.Quartz;

namespace TariffCardService.Worker.Infrastructure
{
	/// <summary>
	/// Регистрация операций периодического повторения.
	/// </summary>
	public static class SchedulerDependencyInjection
	{
		/// <summary>
		/// Регистрирует сервисы периодических задач.
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/>.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddSchedulerServices(this IServiceCollection services)
		{
			services
				.AddTransient<LoadDataJob>()
				.AddSingleton(CreateScheduler)
				.AddHostedService<QuartzHostedService>();

			return services;
		}

		/// <summary>
		/// Создает экземпляр <see cref="IScheduler"/>.
		/// </summary>
		/// <param name="serviceProvider">Service provider.</param>
		/// <returns>Новый экземпляр <see cref="IScheduler"/>.</returns>
		private static IScheduler CreateScheduler(IServiceProvider serviceProvider)
		{
			var quartzSettings = serviceProvider.GetRequiredService<IOptions<SchedulerOptions>>().Value;
			var directoryInstance = DirectSchedulerFactory.Instance;
			directoryInstance.CreateVolatileScheduler(quartzSettings.MaxThreads);

			var scheduler = directoryInstance
				.GetScheduler()
				.GetAwaiter()
				.GetResult();
			LogProvider.IsDisabled = true;

			scheduler.JobFactory = new JobFactory(serviceProvider);
			scheduler.AddJob<LoadDataJob>(quartzSettings.JobActualPoolComplexesRetryInterval);

			return scheduler;
		}

		/// <summary>
		/// Добавить задание в планировщик.
		/// </summary>
		/// <typeparam name="T">Класс исполнителя задания.</typeparam>
		/// <param name="scheduler">Планировщик заданий.</param>
		/// <param name="time">Интервал запуска.</param>
		private static void AddJob<T>(this IScheduler scheduler, TimeSpan time)
			where T : IJob
		{
			scheduler.ScheduleJob(
				JobBuilder.Create<T>().Build(),
				TriggerBuilder.Create().WithSimpleSchedule(s => s.WithInterval(time).RepeatForever()).Build()).Wait();
		}
	}
}
