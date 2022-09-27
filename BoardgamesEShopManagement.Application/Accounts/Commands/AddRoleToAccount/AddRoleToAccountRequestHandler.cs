using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.AddRoleToAccount
{
    public class AddRoleToAccountRequestHandler : IRequestHandler<AddRoleToAccountRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AddRoleToAccountRequestHandler(IUnitOfWork unitOfWork, UserManager<Account> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> Handle(AddRoleToAccountRequest request, CancellationToken cancellationToken)
        {
            Account? searchedAccount = await _unitOfWork.AccountRepository.GetByEmail(request.Email);

            if (searchedAccount == null)
            {
                return false;
            }

            IdentityRole<int> role = await _roleManager.FindByNameAsync(request.RoleName);

            if (role == null)
            {
                await _roleManager.CreateAsync(new IdentityRole<int>
                {
                    Name = request.RoleName
                });
            }

            IdentityResult addRoleToAccount = await _userManager.AddToRoleAsync(searchedAccount, request.RoleName);

            return addRoleToAccount.Succeeded;
        }
    }
}
