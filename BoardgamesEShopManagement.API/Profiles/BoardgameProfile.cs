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
                .ForMember(b => b.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(b => b.Image, opt => opt.MapFrom(s => s.Image))
                .ForMember(b => b.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(b => b.ReleaseYear, opt => opt.MapFrom(s => s.ReleaseYear))
                .ForMember(b => b.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(b => b.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(b => b.Link, opt => opt.MapFrom(s => s.Link))
                .ForMember(b => b.Quantity, opt => opt.MapFrom(s => s.Quantity))
                .ForMember(b => b.CategoryId, opt => opt.MapFrom(s => s.CategoryId));
        }
    }
}
