﻿using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

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
            searchedAccount.Password = "";
            searchedAccount.IsArchived = true;

            searchedAccount.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Save();

            return searchedAccount;
        }
    }
}
