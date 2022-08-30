using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountRequestHandler : IRequestHandler<DeleteAccountRequest, Account>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            Account deletedAccount = await _unitOfWork.AccountRepository.Delete(request.AccountId);

            if (deletedAccount == null)
            {
                return null;
            }

            await _unitOfWork.Save();

            return deletedAccount;
        }
    }
}
