using AutoMapper;

using TariffCardService.Core.Dto;
using TariffCardService.Core.Models;

using TariffCardService.DataAccess.Entities;

using ComplexSnapshot = TariffCardService.DataAccess.Entities.ComplexSnapshot;
using HouseSnapshot = TariffCardService.DataAccess.Entities.HouseSnapshot;
using ObjectSnapshot = TariffCardService.DataAccess.Entities.ObjectSnapshot;
using SearchParamAlias = TariffCardService.Core.Models.SearchParamAlias;
using SnapshotCatalog = TariffCardService.DataAccess.Entities.SnapshotCatalog;

namespace TariffCardService.DataAccess.Mappers
{
	/// <summary>
	/// Класс регистрации настроек автомэппера.
	/// </summary>
	public class TariffCardMapperProfile : Profile
	{
		/// <summary>
		/// Регистрация настроек автомэппера.
		/// </summary>
		public TariffCardMapperProfile()
		{
			CreateMap<CommissionComplex, ComplexDto>();
			CreateMap<CommissionHouseGroup, HouseGroupDto>();
			CreateMap<CommissionObjectGroup, ObjectGroupDto>();

			CreateMap<ComplexSnapshot, ComplexDto>();
			CreateMap<HouseSnapshot, HouseGroupDto>();
			CreateMap<ObjectSnapshot, ObjectGroupDto>();

			CreateMap<Complex, ComplexDto>()
				.ForMember(c => c.Id, mo => mo.Ignore());

			CreateMap<CommissionComplex, Complex>().ReverseMap().ForMember(
				c => c.Id,
				mo => mo.Ignore());

			CreateMap<CommissionHouseGroup, HouseGroup>().ReverseMap()
				.ForMember(
					c => c.Id,
					mo => mo.Ignore())
				.ForMember(
					c => c.ComplexId,
					mo => mo.Ignore());

			CreateMap<CommissionObjectGroup, ObjectGroup>().ReverseMap()
				.ForMember(
					c => c.Id,
					mo => mo.Ignore())
				.ForMember(
					c => c.HouseGroupId,
					mo => mo.Ignore());
			CreateMap<SnapshotCatalog, Core.Models.SnapshotCatalog>().ReverseMap();

			CreateMap<ComplexSnapshot, Core.Models.ComplexSnapshot>().ReverseMap()
				.ForMember(
					c => c.Id,
					mo => mo.Ignore());

			CreateMap<HouseSnapshot, Core.Models.HouseSnapshot>().ReverseMap()
				.ForMember(
					c => c.Id,
					mo => mo.Ignore());

			CreateMap<ObjectSnapshot, Core.Models.ObjectSnapshot>().ReverseMap()
				.ForMember(
					c => c.Id,
					mo => mo.Ignore());

			CreateMap<SearchParamAlias, DataAccess.Entities.SearchParamAlias>().ReverseMap();
		}
	}
}