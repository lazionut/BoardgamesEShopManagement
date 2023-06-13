using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, Account?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account?> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AccountRepository.GetById(request.AccountId);
        }
    }
}