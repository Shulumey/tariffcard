using System.Collections.Generic;

using TariffCardService.Core.Enum;

namespace TariffCardService.Core.Models
{
	/// <summary>
	/// Данные о корпусе.
	/// </summary>
	public class HouseSnapshot
	{
		/// <summary>
		/// Идентификатор корпуса.
		/// </summary>
		public long? HouseId { get; set; }

		/// <summary>
		/// Название корпуса.
		/// </summary>
		public string HouseName { get; set; }

		/// <summary>
		/// Тип комиссии на корпус.
		/// </summary>
		public CommissionType? CommissionType { get; set; }

		/// <summary>
		/// Значение комиссии на корпус.
		/// </summary>
		public decimal? CommissionValue { get; set; }

		/// <summary>
		/// Тип максимальной/минимальной комиссии объектов в корпусе.
		/// </summary>
		public CommissionType? MinMaxCommissionType { get; set; }

		/// <summary>
		/// Значение минимальной комиссии объектов в корпусе.
		/// </summary>
		public decimal? MinCommissionValue { get; set; }

		/// <summary>
		/// Значение максимальной комиссии объектов в корпусе.
		/// </summary>
		public decimal? MaxCommissionValue { get; set; }

		/// <summary>
		/// Количество объектов в комплексе с индивидуальной комисиией.
		/// </summary>
		public int ObjectsCount { get; set; }

		/// <summary>
		/// Значение кроссрегионального коэффициента расширенного бронирования.
		/// </summary>
		public decimal? CrossRegionAdvancedBookingCoefficient { get; set; }

		/// <summary>
		/// Тип объектов в корпусе.
		/// </summary>
		public RealtyObjectType? RealtyObjectType { get; set; }

		/// <summary>
		/// Объекты внутри корпуса.
		/// </summary>
		public ICollection<ObjectSnapshot> ObjectGroups { get; set; }
	}
}