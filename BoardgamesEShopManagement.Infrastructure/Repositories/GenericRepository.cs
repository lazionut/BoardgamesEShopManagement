using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly ShopContext _context;
        private readonly ILogger<GenericRepository<T>> _logger;

        protected GenericRepository(ShopContext context, ILogger<GenericRepository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(T item)
        {
            _logger.LogInformation("Creating {T}", typeof(T).Name);
            await _context.Set<T>().AddAsync(item);
        }

        public async Task<List<T>?> GetAll(int pageIndex, int pageSize)
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }

            _logger.LogInformation("Reading all {T}", typeof(T).Name);
            return await _context.Set<T>().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            _logger.LogInformation("Reading {T} with id {Id}", typeof(T).Name, id);
            return await _context.Set<T>()
                .SingleOrDefaultAsync(item => item.Id == id);
        }

        public void Update(T item)
        {
            _logger.LogInformation("Updating {T} with id {Id}", typeof(T).Name, item.Id);
            _context.Update(item);
        }

        public async Task<T?> Delete(int id)
        {
            _logger.LogInformation("Reading {T} with id {Id}", typeof(T).Name, id);
            T? searchedItem = await _context.Set<T>()
                .SingleOrDefaultAsync(item => item.Id == id);

            if (searchedItem == null)
            {
                _logger.LogError("{T} with id {Id} does not exist", typeof(T).Name, id);
                return null;
            }

            _logger.LogInformation("Deleting {T} with id {Id}", typeof(T).Name, id);
            _context.Set<T>().Remove(searchedItem);

            return searchedItem;
        }
    }
}