using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount
{
    public class GetAccountQuery : IRequest<Account>
    {
        public int AccountId { get; set; }
    }
}