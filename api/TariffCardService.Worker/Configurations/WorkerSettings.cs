namespace TariffCardService.Worker.Configurations
{
	/// <summary>
	/// Секция конфигурации Worker-а.
	/// </summary>
	public class WorkerSettings
	{
		/// <summary>
		/// Название  секции в конфигурации.
		/// </summary>
		public const string SectionName = "WorkerSettings";

		/// <summary>
		///  Cписок застройщиков(компаний), для которых не надо загружать ТК.
		/// </summary>
		public int[] IgnoreSellerIds { get; set; }
	}
}