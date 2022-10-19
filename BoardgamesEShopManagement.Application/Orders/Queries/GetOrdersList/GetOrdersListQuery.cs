using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery : IRequest<List<Order>>
    {
        public int OrderPageIndex { get; set; }
        public int OrderPageSize { get; set; }
    }
}