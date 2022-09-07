using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountsListQuery : IRequest<List<Account>>
    {
        public int AccountPageIndex;
        public int AccountPageSize;
    }
}