using System;
using System.Collections.Generic;

namespace TariffCardService.DataAccess.Entities
{
	/// <summary>
	/// Снимок данных о комплексах, домах и объектах в них, на опредленную дату.
	/// </summary>
	public class SnapshotCatalog
	{
		/// <summary>
		/// Идентификатор снимка данных.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Дата создания снимка данных.
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Тип снимка данных.
		/// </summary>
		public int Type { get; set; }

		/// <inheritdoc cref="Entities.ComplexSnapshot"/>
		public ICollection<ComplexSnapshot> Complexes { get; set; }
	}
}