using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountRequestHandler : IRequestHandler<CreateAccountRequest, Account?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;

        public CreateAccountRequestHandler(IUnitOfWork unitOfWork, UserManager<Account> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Account?> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            Account? searchedAccountByEmail = await _unitOfWork.AccountRepository.GetByEmail(request.AccountEmail);

            if (searchedAccountByEmail != null)
            {
                return null;
            }

            Address? searchedAddress = await _unitOfWork.AddressRepository.GetById(request.AccountAddressId);

            if (searchedAddress == null)
            {
                return null;
            }

            Account? searchedAccountByAddressId = await _unitOfWork.AccountRepository.GetByAddressId(request.AccountAddressId);

            if (searchedAccountByAddressId != null)
            {
                return null;
            }

            string accountUsername = GetUsernameFromEmail(request.AccountEmail);

            Account account = new Account
            {
                FirstName = request.AccountFirstName,
                LastName = request.AccountLastName,
                UserName = accountUsername,
                Email = request.AccountEmail,
                AddressId = request.AccountAddressId,
                IsArchived = false,
            };

            await _userManager.CreateAsync(account, request.AccountPassword);

            return account;
        }

        private string GetUsernameFromEmail(string email) => email.Split('@')[0];
    }
}