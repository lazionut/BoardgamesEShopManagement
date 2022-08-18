using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist
{
    public class DeleteOrderRequestHandler : IRequestHandler<DeleteOrderRequest, bool>
    {
        private readonly IOrderRepository _orderRepository;
        public DeleteOrderRequestHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task<bool> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
        {
            bool isDeleted = _orderRepository.DeleteOrder(request.OrderId);

            return Task.FromResult(isDeleted);
        }
    }
}
