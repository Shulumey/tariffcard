using TariffCardService.Core.Enum;

namespace TariffCardService.Core.Dto
{
	/// <summary>
	/// Тип продавца.
	/// </summary>
	public class SellerTypeComplexQuantityDto
	{
		/// <summary>
		/// Инициализация класса <see cref="SellerTypeComplexQuantityDto"/>.
		/// </summary>
		/// <param name="sellerType">Тип продавца.</param>
		/// <param name="quantity">Количество найденных ЖК по типу продавца.</param>
		public SellerTypeComplexQuantityDto(SellerType sellerType, int quantity)
		{
			SellerType = sellerType;
			Quantity = quantity;
		}

		/// <summary>
		/// Тип продавца.
		/// </summary>
		public SellerType SellerType { get; }

		/// <summary>
		/// Количество найденных ЖК по типу продавца.
		/// </summary>
		public int Quantity { get; }
	}
}