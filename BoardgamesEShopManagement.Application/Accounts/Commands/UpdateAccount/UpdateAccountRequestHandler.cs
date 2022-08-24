using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountRequestHandler : IRequestHandler<UpdateAccountRequest, Account>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
        {
            Account updatedAccount = await _unitOfWork.AccountRepository.UpdateAccount(request.AccountId, request.Account);

            await _unitOfWork.Save();

            return updatedAccount;
        }
    }
}
