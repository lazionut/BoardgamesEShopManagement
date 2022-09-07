using Microsoft.Extensions.Logging;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<Account> _logger;

        public AccountRepository(ShopContext context, ILogger<Account> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
