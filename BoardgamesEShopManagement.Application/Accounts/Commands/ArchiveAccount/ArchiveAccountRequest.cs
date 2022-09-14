using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.ArchiveAccount
{
    public class ArchiveAccountRequest : IRequest<Account>
    {
        public int AccountId { get; set; }
    }
}
