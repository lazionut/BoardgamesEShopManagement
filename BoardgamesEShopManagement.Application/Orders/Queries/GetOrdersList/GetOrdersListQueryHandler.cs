using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<Order>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Order>?> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.OrderRepository.GetAll(request.OrderPageIndex, request.OrderPageSize);
        }
    }
}
