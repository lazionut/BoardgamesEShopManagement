using BoardgamesEShopManagement.Application.Abstract;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsListCounter
{
    public class GetAccountsListCounterQueryHandler : IRequestHandler<GetAccountsListCounterQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountsListCounterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetAccountsListCounterQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AccountRepository.GetAllCounter();
        }
    }
}