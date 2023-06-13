using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.LoginAccount
{
    public class LoginAccountQuery : IRequest<string>
    {
        public string AccountEmail { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
    }
}