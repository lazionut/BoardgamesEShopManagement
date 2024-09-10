using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Options;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.LoginAccount
{
    public class LoginAccountQueryHandler : IRequestHandler<LoginAccountQuery, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;
        private readonly JwtOptions _options;

        public LoginAccountQueryHandler(IUnitOfWork unitOfWork, UserManager<Account> userManager, IOptions<JwtOptions> options)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _options = options.Value;
        }

        public async Task<string> Handle(LoginAccountQuery request, CancellationToken cancellationToken)
        {
            Account? searchedAccount = await _unitOfWork.AccountRepository.GetByEmail(request.AccountEmail);

            if (searchedAccount != null && await _userManager.CheckPasswordAsync(searchedAccount, request.AccountPassword))
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(searchedAccount);

                List<Claim> authClaims = new List<Claim>
                {
                    new Claim("AccountId", searchedAccount.Id.ToString())
                };

                foreach (string userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    authClaims.Add(new Claim("Role", userRole));
                }

                SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _options.ValidIssuer,
                    audience: _options.ValidAudience,
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