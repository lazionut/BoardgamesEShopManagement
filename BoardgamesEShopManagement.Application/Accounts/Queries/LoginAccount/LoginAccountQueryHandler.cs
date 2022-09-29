using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.LoginAccount
{
    public class LoginUserQueryHandler : IRequestHandler<LoginAccountQuery, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;

        public LoginUserQueryHandler(IUnitOfWork unitOfWork, UserManager<Account> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<string> Handle(LoginAccountQuery request, CancellationToken cancellationToken)
        {
            Account? searchedAccount = await _unitOfWork.AccountRepository.GetByEmail(request.AccountEmail);

            if (searchedAccount != null && await _userManager.CheckPasswordAsync(searchedAccount, request.AccountPassword))
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(searchedAccount);

                List<Claim> authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, searchedAccount.Email)
                };

                foreach (string userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                 IConfigurationRoot configuration = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json")
                     .Build();

                SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    claims: authClaims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return "false";
        }
    }
}
