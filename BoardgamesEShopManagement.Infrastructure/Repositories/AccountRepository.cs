using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

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
