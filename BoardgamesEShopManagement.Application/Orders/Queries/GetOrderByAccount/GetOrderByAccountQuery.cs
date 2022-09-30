using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrderByAccount
{
    public class GetOrderByAccountQuery : IRequest<Order>
    {
        public int OrderAccountId { get; set; }
        public int OrderId { get; set; }
    }
}