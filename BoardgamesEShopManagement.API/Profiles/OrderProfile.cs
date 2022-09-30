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
                .ForMember(o => o.CreationDate, opt => opt.MapFrom(s => s.CreatedAt))
                .ForMember(o => o.Boardgames, opt => opt.MapFrom(s => s.OrderItems.Select(
                    oi => new OrderBoardgameDto
                    {
                        Id = oi.Boardgame.Id,
                        Image = oi.Boardgame.Image,
                        Name = oi.Boardgame.Name,
                        Price = oi.Price,
                        Quantity = oi.Quantity,
                    })));

            CreateMap<Order, OrderPostDto>();
        }
    }
}
