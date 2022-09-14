using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrderByAccount
{
    public class GetOrderByAccountQuery : IRequest<Order>
    {
        public int AccountId { get; set; }
        public int OrderId { get; set; }
    }
}