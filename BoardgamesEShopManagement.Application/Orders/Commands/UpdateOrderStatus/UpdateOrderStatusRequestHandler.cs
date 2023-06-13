using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Orders.Commands.UpdateOrderStatus;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder
{
    public class UpdateOrderStatusRequestHandler : IRequestHandler<UpdateOrderStatusRequest, Order?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderStatusRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> Handle(UpdateOrderStatusRequest request, CancellationToken cancellationToken)
        {
            Order? updatedOrder = await _unitOfWork.OrderRepository.GetById(request.OrderId);

            if (updatedOrder == null)
            {
                return null;
            }

            updatedOrder.Status = request.OrderStatus;

            updatedOrder.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.OrderRepository.Update(updatedOrder);
            await _unitOfWork.Save();

            return updatedOrder;
        }
    }
}