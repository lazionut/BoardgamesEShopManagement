using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountRequestHandler : IRequestHandler<DeleteAccountRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            bool isAccountDeleted = await _unitOfWork.AccountRepository.Delete(request.AccountId);

            await _unitOfWork.Save();

            return isAccountDeleted;
        }
    }
}
