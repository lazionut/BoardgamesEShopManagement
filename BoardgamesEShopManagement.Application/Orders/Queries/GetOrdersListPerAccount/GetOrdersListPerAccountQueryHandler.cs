using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListPerAccount
{
    public class GetOrdersListPerAccountQueryHandler : IRequestHandler<GetOrdersListPerAccountQuery, List<Order>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersListPerAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Order>?> Handle(GetOrdersListPerAccountQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.OrderRepository.GetOrdersListPerAccount
                (request.OrderAccountId, request.OrderPageIndex, request.OrderPageSize);
        }
    }
}
