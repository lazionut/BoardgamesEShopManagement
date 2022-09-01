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
                .ForMember(a => a.AddressId, opt => opt.MapFrom(s => s.Id))
                .ForMember(a => a.AddressDetails, opt => opt.MapFrom(s => s.Details))
                .ForMember(a => a.AddressCity, opt => opt.MapFrom(s => s.City))
                .ForMember(a => a.AddressCounty, opt => opt.MapFrom(s => s.County))
                .ForMember(a => a.AddressCountry, opt => opt.MapFrom(s => s.Country))
                .ForMember(a => a.AddressPhone, opt => opt.MapFrom(s => s.Phone));

            CreateMap<Address, AddressPostPutDto>()
                .ForMember(a => a.AddressDetails, opt => opt.MapFrom(s => s.Details))
                .ForMember(a => a.AddressCity, opt => opt.MapFrom(s => s.City))
                .ForMember(a => a.AddressCounty, opt => opt.MapFrom(s => s.County))
                .ForMember(a => a.AddressCountry, opt => opt.MapFrom(s => s.Country))
                .ForMember(a => a.AddressPhone, opt => opt.MapFrom(s => s.Phone));
        }
    }
}
