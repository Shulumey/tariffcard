using TariffCardService.Core.Enum;

namespace TariffCardService.DataAccess.Entities
{
	/// <summary>
	/// Снимок данных представления объектов недвижимости.
	/// </summary>
	public class ObjectSnapshot
	{
		/// <summary>
		/// Идентификатор снимка данных представления объекта.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Внешний ключ на <see cref="Entities.HouseSnapshot"/>.
		/// </summary>
		public long HouseSnapshotId { get; set; }

		/// <summary>
		/// Комнатность объекта.
		/// </summary>
		public int? Rooms { get; set; }

		/// <summary>
		///  Идентификатор объекта в базе НМаркета.
		/// </summary>
		public long? ApartmentId { get; set; }

		/// <summary>
		/// Описание объекта.
		/// </summary>
		public string ApartmentDescription { get; set; }

		/// <summary>
		/// Тип объекта.
		/// </summary>
		public RealtyObjectType? RealtyObjectType { get; set; }

		/// <summary>
		/// Тип комиссиионных, установленных на объект.
		/// </summary>
		public CommissionType CommissionType { get; set; }

		/// <summary>
		/// Значение  комиссиионных, установленных на объект.
		/// </summary>
		public decimal CommissionValue { get; set; }

		/// <summary>
		/// Значение кросс-регионального коэффициента расширенного бронирования
		/// для региона местонахождения объекта.
		/// </summary>
		public decimal CrossRegionAdvancedBookingCoefficient { get; set; }

		/// <summary>
		/// Признак, что комиссионные на объект были переопределены.
		/// </summary>
		public bool IsOverriding { get; set; }
	}
}