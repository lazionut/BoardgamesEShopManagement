using MediatR;

using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListCounter
{
    public class GetOrdersListCounterQueryHandler : IRequestHandler<GetOrdersListCounterQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersListCounterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetOrdersListCounterQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.OrderRepository.GetAllCounter();
        }
    }
}
