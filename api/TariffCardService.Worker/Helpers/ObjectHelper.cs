using TariffCardService.Core.Enum;

namespace TariffCardService.Worker.Helpers
{
    /// <summary>
    /// Класс для временного хранения данных о помещении.
    /// </summary>
    public class ObjectHelper
    {
	    /// <summary>
	    /// Идентификатор корпуса нахождения помещения.
	    /// </summary>
	    public long? HouseId { get; set; }

	    /// <summary>
	    /// Комнатность объекта.
	    /// </summary>
	    public int? Rooms { get; set; }

	    /// <summary>
	    /// Идентификатор объекта (при отсутствии комнатность в корпусе).
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
	    /// Тип комиссии на объект.
	    /// </summary>
	    public CommissionType CommissionType { get; set; }

	    /// <summary>
	    /// Значение комиссии на объект.
	    /// </summary>
	    public decimal CommissionValue { get; set; }

	    /// <summary>
	    /// Значение кросc-регионального коэффициента расширенного бронирования.
	    /// </summary>
	    public decimal CrossRegionAdvancedBookingCoefficient { get; set; }

	    /// <summary>
	    /// Признак, что комиссионные на объект были переопределены.
	    /// </summary>
	    public bool IsOverriding { get; set; }

        /// <summary>
        /// Идентификатор компании-продавца.
        /// </summary>
	    public int? SellerId { get; set; }

        /// <summary>
        /// Тип компании-продавца.
        /// </summary>
	    public SellerType SellerType { get; set; }
    }
}