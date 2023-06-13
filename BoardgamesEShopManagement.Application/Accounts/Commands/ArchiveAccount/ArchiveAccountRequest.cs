using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.ArchiveAccount
{
    public class ArchiveAccountRequest : IRequest<Account>
    {
        public int AccountId { get; set; }
    }
}