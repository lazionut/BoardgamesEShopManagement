using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<Order>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Order>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.OrderRepository.GetAll(request.OrderPageIndex, request.OrderPageSize);
        }
    }
}