using MediatR;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsListCounter
{
    public class GetAccountsListQueryCounterQueryHandler : IRequestHandler<GetAccountsListCounterQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountsListQueryCounterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetAccountsListCounterQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AccountRepository.GetAllCounter();
        }
    }
}
