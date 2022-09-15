using AutoMapper;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressGetDto>()
                .ForMember(a => a.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(a => a.Details, opt => opt.MapFrom(s => s.Details))
                .ForMember(a => a.City, opt => opt.MapFrom(s => s.City))
                .ForMember(a => a.County, opt => opt.MapFrom(s => s.County))
                .ForMember(a => a.Country, opt => opt.MapFrom(s => s.Country))
                .ForMember(a => a.Phone, opt => opt.MapFrom(s => s.Phone));

            CreateMap<Address, AddressPostPutDto>()
                .ForMember(a => a.Details, opt => opt.MapFrom(s => s.Details))
                .ForMember(a => a.City, opt => opt.MapFrom(s => s.City))
                .ForMember(a => a.County, opt => opt.MapFrom(s => s.County))
                .ForMember(a => a.Country, opt => opt.MapFrom(s => s.Country))
                .ForMember(a => a.Phone, opt => opt.MapFrom(s => s.Phone));
        }
    }
}
