using AutoMapper;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryGetDto>()
                .ForMember(c => c.CategoryId, opt => opt.MapFrom(s => s.Id))
                .ForMember(c => c.CategoryName, opt => opt.MapFrom(s => s.Name));

            CreateMap<Category, CategoryPostPutDto>()
                .ForMember(c => c.CategoryName, opt => opt.MapFrom(s => s.Name));
        }
    }
}
