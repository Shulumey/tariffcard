using System.Collections.Generic;

using TariffCardService.Core.Enum;

namespace TariffCardService.Core.Models
{
	/// <summary>
	/// Данные о продавцах и комплексах.
	/// </summary>
	public class Complex
	{
		/// <summary>
		/// Региональная группа.
		/// </summary>
		public int RegionGroupId { get; set; }

		/// <summary>
		/// Идентификатор компании.
		/// </summary>
		public int? SellerId { get; set; }

		/// <summary>
		/// Имя компании.
		/// </summary>
		public string SellerName { get; set; }

		/// <summary>
		/// Тип продавца.
		/// </summary>
		public SellerType SellerType { get; set; }

		/// <summary>
		/// Идентификатор комплекса.
		/// </summary>
		public long? ComplexId { get; set; }

		/// <summary>
		/// Название комплекса.
		/// </summary>
		public string ComplexName { get; set; }

		/// <summary>
		/// Тип комиссии.
		/// </summary>
		public CommissionType? CommissionType { get; set; }

		/// <summary>
		/// Значение минимальной комиссии.
		/// </summary>
		public decimal? MinCommissionValue { get; set; }

		/// <summary>
		/// Значение максимальной комиссии.
		/// </summary>
		public decimal? MaxCommissionValue { get; set; }

		/// <summary>
		/// Признак, что работа с этой компанией предоставляется только в рамках услуги "Расширенное бронирование".
		/// </summary>
		public bool IsAdvancedBooking { get; set; }

		/// <summary>
		/// Признак, что комиссия считается от цены при полной оплате.
		/// </summary>
		public bool IsCommissionCalculatedFromTotalPrice { get; set; }

		/// <summary>
		/// Признак возможности использования расширенного авансирования для компании-продавца.
		/// </summary>
		public bool IsSellerCommissionPrepayments { get; set; }

		/// <summary>
		/// Условия выплаты вознаграждения.
		/// </summary>
		public string ConditionsOfPaymentFees { get; set; }

		/// <summary>
		/// Количество домов в комплексе с объектами внутри.
		/// </summary>
		public int HousesCount { get; set; }

		/// <summary>
		/// Значение кросс-регионального коэффициента расширенного бронирования.
		/// </summary>
		public decimal CrossRegionAdvancedBookingCoefficient { get; set; }

		/// <summary>
		/// Тип объекта в снимке данных комплекса.
		/// </summary>
		public RealtyObjectType RealtyObjectType { get; set; }

		/// <summary>
		/// Url-адрес лендинга авансирования для региона местонахождения комплекса.
		/// </summary>
		public string UrlLandingPrepaymentBooking { get; set; }

		/// <summary>
		/// Корпуса в комплексе.
		/// </summary>
		public ICollection<HouseGroup> HouseGroups { get; set; }
	}
}