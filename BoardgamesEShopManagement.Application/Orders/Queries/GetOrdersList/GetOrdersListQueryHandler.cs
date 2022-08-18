using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, IEnumerable<OrderListVm>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<IEnumerable<OrderListVm>> Handle(GetOrdersListQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<OrderListVm> result = _orderRepository.GetOrders().Select(order => new OrderListVm
            {
                OrderId = order.Id,
                BuyerName = order.BuyerName,
                /*OrderItems = order.OrderItems.Select(boardgame => new OrderItemListDto
                {
                    BoardgameName = boardgame.Boardgame.BoardgameName,
                    Quantity = boardgame.Quantity,
                    Price = boardgame.Boardgame.BoardgamePrice,
                }).ToList()*/
            });

            return Task.FromResult(result);
        }
    }
}
