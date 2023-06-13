using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountRequest : IRequest<Account>
    {
        public int AccountId { get; set; }
        public string AccountFirstName { get; set; } = null!;
        public string AccountLastName { get; set; } = null!;
        public string AccountEmail { get; set; } = null!;
    }
}