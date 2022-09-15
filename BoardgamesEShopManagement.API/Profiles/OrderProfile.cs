using AutoMapper;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderGetDto>()
                .ForMember(o => o.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(o => o.Total, opt => opt.MapFrom(s => s.Total))
                .ForMember(o => o.AccountId, opt => opt.MapFrom(s => s.AccountId))
                .ForMember(o => o.CreationDate, opt => opt.MapFrom(s => s.CreatedAt.Date))
                .ForMember(o => o.Boardgames, opt => opt.MapFrom(s => s.Boardgames.Select(
                    b => new { b.Image, b.Name, b.Price })));

            CreateMap<Order, OrderPostDto>()
                .ForMember(o => o.AccountId, opt => opt.MapFrom(s => s.AccountId));
        }
    }
}
