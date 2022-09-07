using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountRequest : IRequest<Account>
    {
        public int AccountId { get; set; }
    }
}
