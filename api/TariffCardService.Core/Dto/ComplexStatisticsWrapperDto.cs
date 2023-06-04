using System.Collections.Generic;

namespace TariffCardService.Core.Dto
{
	/// <summary>
	/// Комплексы со статистикой.
	/// </summary>
	public class ComplexStatisticsWrapperDto
	{
		/// <summary>
		/// Найденные ЖК.
		/// </summary>
		public IReadOnlyCollection<ComplexDto> Complexes { get; set; }

		/// <summary>
		/// Количество найденных объектов для типов продавцов.
		/// </summary>
		public IReadOnlyCollection<RealtyObjectStatisticsDto> RealtyObjectsStatistic { get; set; }
	}
}