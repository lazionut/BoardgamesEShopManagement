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
                .ForMember(o => o.OrderId, opt => opt.MapFrom(s => s.Id))
                .ForMember(o => o.OrderTotal, opt => opt.MapFrom(s => s.Total))
                .ForMember(o => o.OrderAccountId, opt => opt.MapFrom(s => s.AccountId))
                .ForMember(o => o.OrderBoardgames, opt => opt.MapFrom(s => s.Boardgames.Select(
                    b => new { b.Image, b.Name, b.Description, b.Price, b.Link })));

            CreateMap<Order, OrderPostDto>()
                .ForMember(o => o.OrderAccountId, opt => opt.MapFrom(s => s.AccountId));
        }
    }
}
