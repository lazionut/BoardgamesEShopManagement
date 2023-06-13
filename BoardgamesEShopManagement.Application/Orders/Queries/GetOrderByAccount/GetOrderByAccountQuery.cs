using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrderByAccount
{
    public class GetOrderByAccountQuery : IRequest<Order>
    {
        public int OrderAccountId { get; set; }
        public int OrderId { get; set; }
    }
}