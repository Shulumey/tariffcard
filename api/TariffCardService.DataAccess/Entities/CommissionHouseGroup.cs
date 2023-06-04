using System.Collections.Generic;

using TariffCardService.Core.Enum;

namespace TariffCardService.DataAccess.Entities
{
	/// <summary>
	/// Представление корпуса с данными об установленной комиссии на него,
	/// типе объектов, максимальной и минимальной комиссии объектов в нём.
	/// </summary>
	public class CommissionHouseGroup
	{
		/// <summary>
		/// Идентификатор представления корпуса.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Внешний ключ на <see cref="Entities.CommissionComplex"/>.
		/// </summary>
		public long ComplexId { get; set; }

		/// <summary>
		/// Идентификатор корпуса в базе НМаркета.
		/// </summary>
		public long? HouseId { get; set; }

		/// <summary>
		/// Название корпуса в базе НМаркета.
		/// </summary>
		public string HouseName { get; set; }

		/// <summary>
		/// Тип комиссиионных, установленных на корпус.
		/// </summary>
		public CommissionType? CommissionType { get; set; }

		/// <summary>
		/// Значение комиссиионных, установленных на корпус.
		/// </summary>
		public decimal? CommissionValue { get; set; }

		/// <summary>
		/// Тип комиссиионных, установленных на объекты в корпусе,
		/// при их наличии, в порядке CommissionType.Percent=>AbsolutePerSqMeter=>Absolute.
		/// </summary>
		public CommissionType? MinMaxCommissionType { get; set; }

		/// <summary>
		/// Минимальное значение комиссиионных, установленных на объекты в корпусе.
		/// </summary>
		public decimal? MinCommissionValue { get; set; }

		/// <summary>
		/// Максимальное значение комиссиионных, установленных на объекты в корпусе.
		/// </summary>
		public decimal? MaxCommissionValue { get; set; }

		/// <summary>
		/// Количество объектов в корпусе с индивидуальной комисиией.
		/// </summary>
		public int ObjectsCount { get; set; }

		/// <summary>
		/// Значение кросс-регионального коэффициента расширенного бронирования
		/// для региона местонахождения корпуса.
		/// </summary>
		public decimal CrossRegionAdvancedBookingCoefficient { get; set; }

		/// <summary>
		/// Тип объектов в корпусе.
		/// </summary>
		public RealtyObjectType RealtyObjectType { get; set; }

		/// <summary>
		/// Признак наличия объектов с переопределенными комисионными в корпусе.
		/// </summary>
		public bool HasOverriding { get; set; }

		/// <inheritdoc cref="CommissionObjectGroup"/>
		public ICollection<CommissionObjectGroup> ObjectGroups { get; set; }
	}
}