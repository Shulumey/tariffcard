using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using TariffCardService.Core.Dto;
using TariffCardService.Core.Enum;
using TariffCardService.Core.Interfaces.Data;
using TariffCardService.Core.Models;

namespace TariffCardService.Business.Features.Complexes.Command
{
	/// <summary>
	/// Получение основной тарифной карты.
	/// </summary>
	public static class GetComplexes
	{
		/// <inheritdoc />
		public sealed class Command : IRequest<ComplexStatisticsWrapperDto>
		{
			/// <summary>
			/// Региональная группа запрашиваемого снимка данных.
			/// </summary>
			public int RegionGroupId { get; set; }

			/// <summary>
			/// Тип объектов в снимке данных.
			/// </summary>
			public RealtyObjectType RealtyObjectType { get; set; }

			/// <summary>
			/// Тип продавца в снимке данных.
			/// </summary>
			public SellerType SellerType { get; set; }

			/// <summary>
			/// Поисковая строка.
			/// </summary>
			public string SearchString { get; set; }
		}

		/// <inheritdoc />
		public sealed class Handler : IRequestHandler<Command, ComplexStatisticsWrapperDto>
		{
			/// <inheritdoc cref="IComplexProvider"/>
			private readonly IComplexProvider _complexProvider;

			/// <inheritdoc cref="ISearchParamAliasProvider"/>
			private readonly ISearchParamAliasProvider _searchStringsProvider;

			/// <inheritdoc cref="IMapper"/>
			private readonly IMapper _mapper;

			/// <summary>
			/// Инициализирует новый экземпляр класса <see cref="Handler"/>.
			/// </summary>
			/// <param name="complexProvider"><see cref="IComplexProvider"/>.</param>
			/// <param name="searchStringsProvider"><see cref="ISearchParamAliasProvider"/>.</param>
			/// <param name="mapper"><see cref="IMapper"/>.</param>
			public Handler(IComplexProvider complexProvider, ISearchParamAliasProvider searchStringsProvider, IMapper mapper)
			{
				_complexProvider = complexProvider;
				_searchStringsProvider = searchStringsProvider;
				_mapper = mapper;
			}

			/// <inheritdoc />
			public async Task<ComplexStatisticsWrapperDto> Handle(Command request, CancellationToken cancellationToken)
			{
				List<string> allStrings = new List<string>();

				var objectTypes = request.RealtyObjectType == RealtyObjectType.Apartment
					? new List<RealtyObjectType> { RealtyObjectType.Apartment, RealtyObjectType.CommercialApartment }
					: new List<RealtyObjectType> { request.RealtyObjectType };

				if (!string.IsNullOrEmpty(request.SearchString?.Trim()))
				{
					IEnumerable<string> searchStrings = await _searchStringsProvider.GetSearchParamAliasActualValuesAsync(request.SearchString, request.RegionGroupId, cancellationToken);
					allStrings.AddRange(searchStrings);
					allStrings.Add(request.SearchString);
				}

				IReadOnlyCollection<Complex> complexes = await _complexProvider.GetComplexesAsync(
					request.RegionGroupId,
					objectTypes,
					request.SellerType,
					allStrings,
					cancellationToken);

				IReadOnlyCollection<RealtyObjectStatisticsDto> realtyObjectStatistics = complexes
					.GroupBy(x => x.RealtyObjectType)
					.Select(complexCommissionRealtyObject => new RealtyObjectStatisticsDto(complexCommissionRealtyObject.Key, complexCommissionRealtyObject
						.GroupBy(x => x.SellerType)
						.ToDictionary(x => x.Key, x => x.Count())
						.Select(x => new SellerTypeComplexQuantityDto(x.Key, x.Value))
						.ToArray()))
					.ToArray();

				return new ComplexStatisticsWrapperDto
				{
					Complexes = complexes.Select(cc => _mapper.Map<ComplexDto>(cc)).ToArray(),
					RealtyObjectsStatistic = realtyObjectStatistics,
				};
			}
		}
	}
}