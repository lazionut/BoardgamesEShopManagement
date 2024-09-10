using MediatR;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListPerAccountCounter
{
    public class GetOrdersListPerAccountCounterQuery : IRequest<int>
    {
        public int OrderAccountId { get; set; }
    }
}