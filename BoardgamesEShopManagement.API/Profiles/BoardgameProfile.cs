using AutoMapper;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Profiles
{
    public class BoardgameProfile : Profile
    {
        public BoardgameProfile()
        {
            CreateMap<Boardgame, BoardgameGetDto>()
                .ForMember(b => b.BoardgameId, opt => opt.MapFrom(s => s.Id))
                .ForMember(b => b.BoardgameImage, opt => opt.MapFrom(s => s.Image))
                .ForMember(b => b.BoardgameName, opt => opt.MapFrom(s => s.Name))
                .ForMember(b => b.BoardgameDescription, opt => opt.MapFrom(s => s.Description))
                .ForMember(b => b.BoardgamePrice, opt => opt.MapFrom(s => s.Price))
                .ForMember(b => b.BoardgameLink, opt => opt.MapFrom(s => s.Link))
                .ForMember(b => b.BoardgameCategoryId, opt => opt.MapFrom(s => s.CategoryId));
        }
    }
}
