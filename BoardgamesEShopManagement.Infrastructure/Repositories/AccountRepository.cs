using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using System.Xml.Linq;
using BoardgamesEShopManagement.Application.Abstract;
using MediatR;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<Account> _logger;

        public AccountRepository(ShopContext context, ILogger<Account> logger) 
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Account?> GetByEmail(string email)
        {
            return await _context.Accounts.SingleOrDefaultAsync(account => account.Email == email);
        }
        public async Task<Account?> GetByAddressId(int addressId)
        {
            return await _context.Accounts.SingleOrDefaultAsync(account => account.AddressId == addressId);
        }

        public async Task Create(Account account)
        {
            _logger.LogInformation($"Preparing to add {typeof(Account)} to the database...");
            await _context.Accounts.AddAsync(account);
        }

        public async Task<List<Account>?> GetAll(int pageIndex, int pageSize)
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }

            _logger.LogInformation($"Getting the list of {typeof(Account)}...");
            return await _context.Accounts.Include(account => account.Address).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Account?> GetById(int id)
        {
            _logger.LogInformation($"Getting {typeof(Account)} by it's identifier...");
            return await _context.Accounts
                .Include(account => account.Address)
                .SingleOrDefaultAsync(item => item.Id == id);
        }

        public async Task Update(Account account)
        {
            _logger.LogInformation($"Preparing to update {typeof(Account)} from the database...");
            _context.Update(account);
        }

        public async Task<Account?> Delete(int id)
        {
            _logger.LogInformation($"Trying to get {typeof(Account)} by it's identifier...");
            Account? searchedItem = await _context.Accounts
                .SingleOrDefaultAsync(item => item.Id == id);

            if (searchedItem == null)
            {
                _logger.LogError($"Could not find {typeof(Account)}.");
                return null;
            }

            _logger.LogInformation($"Preparing to remove {typeof(Account)} from the database...");
            _context.Accounts.Remove(searchedItem);

            return searchedItem;
        }

        public async Task Save()
        {
            _logger.LogInformation("Saving current changes to the database...");
            await _context.SaveChangesAsync();
        }
    }
}
