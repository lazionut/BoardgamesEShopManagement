using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlist
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            orderRepository = _orderRepository;
        }

        public Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            Order result = _orderRepository.GetOrder(request.OrderId);

            return Task.FromResult(result);
        }
    }
}
