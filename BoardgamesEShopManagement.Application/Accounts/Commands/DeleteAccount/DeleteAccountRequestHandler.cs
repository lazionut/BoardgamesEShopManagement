using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountRequestHandler : IRequestHandler<DeleteAccountRequest, Account?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account?> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            Account? deletedAccount = await _unitOfWork.AccountRepository.Delete(request.AccountId);

            if (deletedAccount == null)
            {
                return null;
            }

            await _unitOfWork.Save();

            return deletedAccount;
        }
    }
}