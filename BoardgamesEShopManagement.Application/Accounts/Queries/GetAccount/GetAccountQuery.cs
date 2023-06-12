using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount
{
    public class GetAccountQuery : IRequest<Account>
    {
        public int AccountId { get; set; }
    }
}