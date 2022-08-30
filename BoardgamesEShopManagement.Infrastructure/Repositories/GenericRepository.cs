using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly ShopContext _context;

        public GenericRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task Create(T item)
        {
            await _context.Set<T>().AddAsync(item);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>()
                .SingleOrDefaultAsync(item => item.Id == id);
        }

        public async Task Update(T item)
        {
            _context.Update(item);
        }

        public async Task<T> Delete(int id)
        {
            T searchedItem = await _context.Set<T>()
                .SingleOrDefaultAsync(item => item.Id == id);

            if (searchedItem == null)
            {
                return null;
            }

            _context.Set<T>().Remove(searchedItem);

            return searchedItem;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
