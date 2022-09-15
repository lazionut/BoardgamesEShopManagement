using AutoMapper;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountGetDto>()
                .ForMember(a => a.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(a => a.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(a => a.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(a => a.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(a => a.Password, opt => opt.MapFrom(s => s.Password))
                .ForMember(a => a.AddressId, opt => opt.MapFrom(s => s.AddressId));

            CreateMap<Account, AccountPostDto>()
               .ForMember(a => a.FirstName, opt => opt.MapFrom(s => s.FirstName))
               .ForMember(a => a.LastName, opt => opt.MapFrom(s => s.LastName))
               .ForMember(a => a.Email, opt => opt.MapFrom(s => s.Email))
               .ForMember(a => a.Password, opt => opt.MapFrom(s => s.Password));
        }
    }
}
