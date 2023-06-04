using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TariffCardService.Core.Enum;

namespace TariffCardService.DataAccess.Entities
{
	/// <summary>
	/// Представление комплекса с данными о продавце, типе объектов,
	/// максимальной и минимальной комиссии в нём,
	/// и данными о корпусах внутри него.
	/// </summary>
	public class CommissionComplex
	{
		/// <summary>
		/// Идентификатор представления комплекса.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Региональная группа местонахождения комплекса.
		/// </summary>
		public int RegionGroupId { get; set; }

		/// <summary>
		/// Идентификатор компании-продавца в базе НМаркета.
		/// </summary>
		public int SellerId { get; set; }

		/// <summary>
		/// Наименование компании-продавца.
		/// </summary>
		public string SellerName { get; set; }

		/// <summary>
		/// Тип продавца.
		/// </summary>
		public SellerType SellerType { get; set; }

		/// <summary>
		/// Идентификатор комплекса в базе НМаркета.
		/// </summary>
		public long ComplexId { get; set; }

		/// <summary>
		/// Название комплекса в базе НМаркета.
		/// </summary>
		public string ComplexName { get; set; }

		/// <summary>
		/// Тип комиссиионных.
		/// </summary>
		public CommissionType? CommissionType { get; set; }

		/// <summary>
		/// Минимальное значение комиссионных внутри комплекса
		/// (при наличии одинакового типа комиссионных в корпусах).
		/// </summary>
		public decimal? MinCommissionValue { get; set; }

		/// <summary>
		/// Максимальное значение комиссионных внутри комплекса
		/// (при наличии одинакового типа комиссионных в корпусах).
		/// </summary>
		public decimal? MaxCommissionValue { get; set; }

		/// <summary>
		/// Разрешение расширенного бронирования для компании-продавца.
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
		/// Условия выплаты вознаграждения у компании-продавца.
		/// </summary>
		public string ConditionsOfPaymentFees { get; set; }

		/// <summary>
		/// Количество корпусов в комплексе с объектами-исключениями внутри.
		/// </summary>
		public int HousesCount { get; set; }

		/// <summary>
		/// Значение кросс-регионального коэффициента расширенного бронирования
		/// для региона местонахождения комплекса.
		/// </summary>
		public decimal CrossRegionAdvancedBookingCoefficient { get; set; }

		/// <summary>
		/// Лендинг авансирования для региона местонахождения комплекса.
		/// </summary>
		public string UrlLandingPrepaymentBooking { get; set; }

		/// <summary>
		/// Тип объектов в комплексе.
		/// </summary>
		public RealtyObjectType RealtyObjectType { get; set; }

		/// <inheritdoc cref="Entities.CommissionHouseGroup"/>
		public ICollection<CommissionHouseGroup> HouseGroups { get; set; }
	}
}