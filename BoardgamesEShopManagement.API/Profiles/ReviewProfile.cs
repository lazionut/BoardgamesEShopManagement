using AutoMapper;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewGetDto>()
                .ForMember(r => r.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(r => r.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(r => r.Score, opt => opt.MapFrom(s => s.Score))
                .ForMember(r => r.Author, opt => opt.MapFrom(s => s.Author))
                .ForMember(r => r.Content, opt => opt.MapFrom(s => s.Content))
                .ForMember(r => r.BoardgameId, opt => opt.MapFrom(s => s.BoardgameId))
                .ForMember(r => r.AccountId, opt => opt.MapFrom(s => s.AccountId))
                .ForMember(r => r.CreationDate, opt => opt.MapFrom(s => s.CreatedAt.Date));

            CreateMap<Review, ReviewPostDto > ()
                   .ForMember(r => r.Title, opt => opt.MapFrom(s => s.Title))
                   .ForMember(r => r.Score, opt => opt.MapFrom(s => s.Score))
                   .ForMember(r => r.Author, opt => opt.MapFrom(s => s.Author))
                   .ForMember(r => r.Content, opt => opt.MapFrom(s => s.Content))
                   .ForMember(r => r.BoardgameId, opt => opt.MapFrom(s => s.BoardgameId))
                   .ForMember(r => r.AccountId, opt => opt.MapFrom(s => s.AccountId));

            CreateMap<Review, ReviewPatchDto>()
                   .ForMember(r => r.Title, opt => opt.MapFrom(s => s.Title))
                   .ForMember(r => r.Content, opt => opt.MapFrom(s => s.Content));
        }
    }
}
