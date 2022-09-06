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
                .ForMember(r => r.ReviewId, opt => opt.MapFrom(s => s.Id))
                .ForMember(r => r.ReviewTitle, opt => opt.MapFrom(s => s.Title))
                .ForMember(r => r.ReviewScore, opt => opt.MapFrom(s => s.Score))
                .ForMember(r => r.ReviewAuthor, opt => opt.MapFrom(s => s.Author))
                .ForMember(r => r.ReviewContent, opt => opt.MapFrom(s => s.Content))
                .ForMember(r => r.ReviewBoardgameId, opt => opt.MapFrom(s => s.BoardgameId))
                .ForMember(r => r.ReviewAccountId, opt => opt.MapFrom(s => s.AccountId))
                .ForMember(r => r.ReviewCreatedAt, opt => opt.MapFrom(s => s.CreatedAt.Date));

            CreateMap<Review, ReviewPostDto>()
                   .ForMember(r => r.ReviewTitle, opt => opt.MapFrom(s => s.Title))
                   .ForMember(r => r.ReviewScore, opt => opt.MapFrom(s => s.Score))
                   .ForMember(r => r.ReviewAuthor, opt => opt.MapFrom(s => s.Author))
                   .ForMember(r => r.ReviewContent, opt => opt.MapFrom(s => s.Content))
                   .ForMember(r => r.ReviewBoardgameId, opt => opt.MapFrom(s => s.BoardgameId))
                   .ForMember(r => r.ReviewAccountId, opt => opt.MapFrom(s => s.AccountId));

            CreateMap<Review, ReviewPatchDto>()
                   .ForMember(r => r.ReviewTitle, opt => opt.MapFrom(s => s.Title))
                   .ForMember(r => r.ReviewContent, opt => opt.MapFrom(s => s.Content));
        }
    }
}
