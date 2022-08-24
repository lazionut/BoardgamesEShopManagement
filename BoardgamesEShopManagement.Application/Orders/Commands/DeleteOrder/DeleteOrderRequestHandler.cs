using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Orders.Commands.DeleteWishlist
{
    public class DeleteOrderRequestHandler : IRequestHandler<DeleteOrderRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
        {
            bool isOrderDeleted = await _unitOfWork.OrderRepository.Delete(request.OrderId);

            await _unitOfWork.Save();

            return isOrderDeleted;
        }
    }
}
