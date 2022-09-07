using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetOrdersListPerAccount
{
    public class GetOrdersListPerAccountQuery : IRequest<List<Order>>
    {
        public int OrderAccountId { get; set; }
        public int OrderPageIndex { get; set; }
        public int OrderPageSize { get; set; }
    }
}