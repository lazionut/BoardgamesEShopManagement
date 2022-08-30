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
            Account updatedAccount = await _unitOfWork.AccountRepository.GetById(request.AccountId);

            if (updatedAccount == null)
            {
                //await Task.FromResult(null);
                return null;
            }

            updatedAccount.FirstName = request.AccountFirstName;
            updatedAccount.LastName = request.AccountLastName;
            updatedAccount.Email = request.AccountEmail;
            updatedAccount.Password = request.AccountPassword;

            await _unitOfWork.AccountRepository.Update(updatedAccount);

            await _unitOfWork.Save();

            return updatedAccount;
        }
    }
}
