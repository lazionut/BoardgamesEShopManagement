using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListPerAccount
{
    public class GetOrdersListPerAccountQuery : IRequest<List<Order>>
    {
        public int OrderAccountId { get; set; }
        public int OrderPageIndex { get; set; }
        public int OrderPageSize { get; set; }
    }
}