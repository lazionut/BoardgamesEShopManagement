using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<Order>
    {
        public int OrderId { get; set; }
    }
}