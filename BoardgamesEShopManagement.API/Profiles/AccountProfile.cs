using AutoMapper;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountGetDto>();

            CreateMap<Account, AccountAdminGetDto>();

            CreateMap<Account, AccountPostDto>();
        }
    }
}
