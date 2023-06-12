using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.GetByEmail
{
    public class GetAccountByEmailQuery : IRequest<Account>
    {
        public string AccountEmail { get; set; } = null!;
    }
}