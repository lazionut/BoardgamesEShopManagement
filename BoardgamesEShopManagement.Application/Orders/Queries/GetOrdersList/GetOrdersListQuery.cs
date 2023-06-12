using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery : IRequest<List<Order>>
    {
        public int OrderPageIndex { get; set; }
        public int OrderPageSize { get; set; }
    }
}