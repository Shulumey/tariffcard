using System.Collections.Generic;

using TariffCardService.Core.Enum;

namespace TariffCardService.Core.Dto
{
	/// <summary>
	///  Статистика для по типу объекта.
	/// </summary>
	public class RealtyObjectStatisticsDto
	{
		/// <summary>
		/// Инициализация класс <see cref="RealtyObjectStatisticsDto"/>.
		/// </summary>
		/// <param name="objectType">Тип объекта.</param>
		/// <param name="sellersStatistics">Статистика по типу продавца.</param>
		public RealtyObjectStatisticsDto(RealtyObjectType objectType, IReadOnlyCollection<SellerTypeComplexQuantityDto> sellersStatistics)
		{
			ObjectType = objectType;
			SellersStatistic = sellersStatistics;
		}

		/// <summary>
		/// Тип объекта.
		/// </summary>
		public RealtyObjectType ObjectType { get; }

		/// <summary>
		/// Статистика по типу продавца.
		/// </summary>
		public IReadOnlyCollection<SellerTypeComplexQuantityDto> SellersStatistic { get; }
	}
}