using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.OrderRepository.GetById(request.OrderId);
        }
    }
}