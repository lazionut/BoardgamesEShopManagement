using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderRequestHandler : IRequestHandler<DeleteOrderRequest, Order>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
        {
            Order deletedOrder = await _unitOfWork.OrderRepository.Delete(request.OrderId);

            if (deletedOrder == null)
            {
                return null;
            }

            await _unitOfWork.Save();

            return deletedOrder;
        }
    }
}
