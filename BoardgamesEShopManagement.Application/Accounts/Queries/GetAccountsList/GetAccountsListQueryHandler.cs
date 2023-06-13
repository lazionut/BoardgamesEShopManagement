using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountsListQueryHandler : IRequestHandler<GetAccountsListQuery, List<Account>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountsListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Account>?> Handle(GetAccountsListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AccountRepository.GetAll(request.AccountPageIndex, request.AccountPageSize);
        }
    }
}