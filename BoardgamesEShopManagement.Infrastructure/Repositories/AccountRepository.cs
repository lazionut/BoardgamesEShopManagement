using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(ShopContext context, ILogger<AccountRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Account?> GetByEmail(string email)
        {
            _logger.LogInformation("Reading {Account} with email {Email}", typeof(Account).Name, email);
            return await _context.Accounts.SingleOrDefaultAsync(account => account.Email == email);
        }

        public async Task<Account?> GetByAddressId(int addressId)
        {
            _logger.LogInformation("Reading {Account} with address id {AddressId}", typeof(Account).Name, addressId);
            return await _context.Accounts.SingleOrDefaultAsync(account => account.AddressId == addressId);
        }

        public async Task Create(Account account)
        {
            _logger.LogInformation("Creating {Account}", typeof(Account).Name);
            await _context.Accounts.AddAsync(account);
        }

        public async Task<List<Account>?> GetAll(int pageIndex, int pageSize)
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }

            _logger.LogInformation("Reading all {Account}", typeof(Account).Name);
            return await _context.Accounts
                .Include(account => account.Address)
                .OrderByDescending(account => account.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetAllCounter()
        {
            _logger.LogInformation("Reading number of {Account}", typeof(Account).Name);
            return await _context.Accounts.CountAsync();
        }

        public async Task<Account?> GetById(int id)
        {
            _logger.LogInformation("Reading {Account} with id {Id}", typeof(Account).Name, id);
            return await _context.Accounts
                .Include(account => account.Address)
                .SingleOrDefaultAsync(item => item.Id == id);
        }

        public void Update(Account account)
        {
            _logger.LogInformation("Updating {Account} with id {Id}", typeof(Account).Name, account.Id);
            _context.Update(account);
        }

        public async Task<Account?> Delete(int id)
        {
            _logger.LogInformation("Deleting {Account} with id {Id}", typeof(Account).Name, id);
            Account? searchedItem = await _context.Accounts
                .SingleOrDefaultAsync(item => item.Id == id);

            if (searchedItem == null)
            {
                _logger.LogError("{Account} with id {Id} does not exist", typeof(Account).Name, id);
                return null;
            }

            _logger.LogInformation("Removing {Account} with {Id}", typeof(Account).Name, id);
            _context.Accounts.Remove(searchedItem);

            return searchedItem;
        }
    }
}