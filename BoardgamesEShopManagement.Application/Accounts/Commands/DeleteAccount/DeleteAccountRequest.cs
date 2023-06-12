using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountRequest : IRequest<Account>
    {
        public int AccountId { get; set; }
    }
}