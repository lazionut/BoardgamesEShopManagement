using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

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
