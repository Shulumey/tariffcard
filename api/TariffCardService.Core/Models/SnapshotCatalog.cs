using System;
using System.Collections.Generic;

namespace TariffCardService.Core.Models
{
	/// <summary>
	/// Каталог снимков данных по тарифной карте.
	/// </summary>
	public class SnapshotCatalog
	{
		/// <summary>
		/// Идентификатор снимка данных.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Дата, на которую сделан снимок данных.
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Тип снимка данных.
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// Снимки данных комплексов с данными продавцов.
		/// </summary>
		public ICollection<ComplexSnapshot> Complexes { get; set; }
	}
}