using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.ArchiveAccount
{
    public class ArchiveAccountRequestHandler : IRequestHandler<ArchiveAccountRequest, Account?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveAccountRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account?> Handle(ArchiveAccountRequest request, CancellationToken cancellationToken)
        {
            Account? searchedAccount = await _unitOfWork.AccountRepository.GetById(request.AccountId);

            if (searchedAccount == null)
            {
                return null;
            }

            searchedAccount.FirstName = "Anonymized";
            searchedAccount.LastName = "Anonymized";
            searchedAccount.Email = "Anonymized";
            searchedAccount.IsArchived = true;

            searchedAccount.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Save();

            return searchedAccount;
        }
    }
}