using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.GetByEmail
{
    public class GetAccountByEmailQueryHandler : IRequestHandler<GetAccountByEmailQuery, Account?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountByEmailQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account?> Handle(GetAccountByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AccountRepository.GetByEmail(request.AccountEmail);
        }
    }
}