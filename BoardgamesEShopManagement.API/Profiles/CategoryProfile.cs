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
                .ForMember(c => c.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(c => c.Name, opt => opt.MapFrom(s => s.Name));

            CreateMap<Category, CategoryPostPutDto>()
                .ForMember(c => c.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}
