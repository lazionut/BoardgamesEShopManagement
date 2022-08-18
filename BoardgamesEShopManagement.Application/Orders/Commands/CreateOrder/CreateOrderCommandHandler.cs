using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrder, int>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<int> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            IEnumerable<OrderItem> orderItems = request.OrderItems.Select(orderItemDto =>
                new OrderItem {Boardgame = new Boardgame { BoardgameName = orderItemDto.BoardgameName, BoardgamePrice = orderItemDto.Price }, Quantity = orderItemDto.Quantity });

            Order order = new Order { BuyerName = request.BuyerName };
            _orderRepository.CreateOrder(order);

            return Task.FromResult(order.Id);
        }
    }
}
