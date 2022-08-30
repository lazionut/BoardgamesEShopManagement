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
                .ForMember(a => a.AccountId, opt => opt.MapFrom(s => s.Id))
                .ForMember(a => a.AccountFirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(a => a.AccountLastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(a => a.AccountEmail, opt => opt.MapFrom(s => s.Email))
                .ForMember(a => a.AccountPassword, opt => opt.MapFrom(s => s.Password))
                .ForMember(a => a.AccountAddressId, opt => opt.MapFrom(s => s.AddressId));

            CreateMap<Account, AccountPostDto>()
               .ForMember(a => a.AccountFirstName, opt => opt.MapFrom(s => s.FirstName))
               .ForMember(a => a.AccountLastName, opt => opt.MapFrom(s => s.LastName))
               .ForMember(a => a.AccountEmail, opt => opt.MapFrom(s => s.Email))
               .ForMember(a => a.AccountPassword, opt => opt.MapFrom(s => s.Password));
        }
    }
}
