using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly ShopContext _context;
        private readonly ILogger<T> _logger;

        public GenericRepository(ShopContext context, ILogger<T> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(T item)
        {
            _logger.LogInformation($"Preparing to add {typeof(T)} to the database...");
            await _context.Set<T>().AddAsync(item);
        }

        public async Task<List<T>?> GetAll(int pageIndex, int pageSize)
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }

            _logger.LogInformation($"Getting the list of {typeof(T)}...");
            return await _context.Set<T>().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            _logger.LogInformation($"Getting {typeof(T)} by it's identifier...");
            return await _context.Set<T>()
                .SingleOrDefaultAsync(item => item.Id == id);
        }

        public async Task Update(T item)
        {
            _logger.LogInformation($"Preparing to update {typeof(T)} from the database...");
            _context.Update(item);
        }

        public async Task<T?> Delete(int id)
        {
            _logger.LogInformation($"Trying to get {typeof(T)} by it's identifier...");
            T? searchedItem = await _context.Set<T>()
                .SingleOrDefaultAsync(item => item.Id == id);

            if (searchedItem == null)
            {
                _logger.LogError($"Could not find {typeof(T)}.");
                return null;
            }

            _logger.LogInformation($"Preparing to remove {typeof(T)} from the database...");
            _context.Set<T>().Remove(searchedItem);

            return searchedItem;
        }

        public async Task Save()
        {
            _logger.LogInformation("Saving current changes to the database...");
            await _context.SaveChangesAsync();
        }
    }
}
