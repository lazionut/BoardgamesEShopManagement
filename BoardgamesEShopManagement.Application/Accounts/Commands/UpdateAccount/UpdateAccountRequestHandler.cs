using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountRequestHandler : IRequestHandler<UpdateAccountRequest, Account?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account?> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
        {
            Account? searchedAccount = await _unitOfWork.AccountRepository.GetById(request.AccountId);

            if (searchedAccount == null)
            {
                return null;
            }

            searchedAccount.FirstName = request.AccountFirstName;
            searchedAccount.LastName = request.AccountLastName;

            if (searchedAccount.Email != request.AccountEmail)
            {
                Account? searchedAccountEmail = await _unitOfWork.AccountRepository.GetByEmail(request.AccountEmail);

                if (searchedAccountEmail != null)
                {
                    return null;
                }

                searchedAccount.Email = request.AccountEmail;
            }

            searchedAccount.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.AccountRepository.Update(searchedAccount);
            await _unitOfWork.Save();

            return searchedAccount;
        }
    }
}