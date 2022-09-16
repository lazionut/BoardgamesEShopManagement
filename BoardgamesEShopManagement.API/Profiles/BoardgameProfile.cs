using AutoMapper;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Profiles
{
    public class BoardgameProfile : Profile
    {
        public BoardgameProfile()
        {
            CreateMap<Boardgame, BoardgameGetDto>();
        }
    }
}
