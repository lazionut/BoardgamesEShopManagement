using MediatR;

using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListPerAccountCounter
{
    public class GetOrdersListPerAccountCounterQueryHandler : IRequestHandler<GetOrdersListPerAccountCounterQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersListPerAccountCounterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetOrdersListPerAccountCounterQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.OrderRepository.GetPerAccountCounter(request.OrderAccountId);
        }
    }
}
