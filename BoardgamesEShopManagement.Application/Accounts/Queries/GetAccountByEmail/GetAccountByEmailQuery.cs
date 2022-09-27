using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.GetByEmail
{
    public class GetAccountByEmailQuery : IRequest<Account>
    {
        public string AccountEmail { get; set; } = null!;
    }
}