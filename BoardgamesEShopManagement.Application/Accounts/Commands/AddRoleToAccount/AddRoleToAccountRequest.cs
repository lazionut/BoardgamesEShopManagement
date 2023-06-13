using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.AddRoleToAccount
{
    public class AddRoleToAccountRequest : IRequest<bool>
    {
        public string Email { get; set; } = null!;
        public string RoleName { get; set; } = null!;
    }
}