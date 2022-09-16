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
                    b => new OrderBoardgameDto { Id = b.Id, Image = b.Image, Name = b.Name, Price = b.Price })));

            CreateMap<Order, OrderPostDto>();
        }
    }
}
