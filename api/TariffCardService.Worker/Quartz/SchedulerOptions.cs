using System;

namespace TariffCardService.Worker.Quartz
{
	/// <summary>
	/// Настройки операций периодического повторения.
	/// </summary>
	public class SchedulerOptions
	{
		/// <summary>
		/// Количество потоков выполнения.
		/// </summary>
		public int MaxThreads { get; set; } = 1;

		/// <summary>
		/// Интервал повторения операции.
		/// </summary>
		public TimeSpan JobActualPoolComplexesRetryInterval { get; set; } = TimeSpan.Parse("01:00:00");
	}
}
