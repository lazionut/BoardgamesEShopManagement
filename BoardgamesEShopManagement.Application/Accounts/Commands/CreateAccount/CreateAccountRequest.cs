using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountRequest : IRequest<Account>
    {
        public string AccountEmail { get; set; } = null!;
        public string AccountFirstName { get; set; } = null!;
        public string AccountLastName { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
        public int AccountAddressId { get; set; }
    }
}