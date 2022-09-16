using AutoMapper;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewGetDto>();

            CreateMap<Review, ReviewPostDto>();

            CreateMap<Review, ReviewPatchDto>();
        }
    }
}
