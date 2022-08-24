using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Exceptions;

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
            if (item == null)
                throw new GenericItemException($"{item} can\'t be created!");

            _context.Set<T>().AddAsync(item);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            if (id >= 0)
            {
                return await _context.Set<T>().SingleOrDefaultAsync(item => item.Id == id);
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task<bool> Delete(int id)
        {
            if (id >= 0)
            {
                T searchedItem = await _context.Set<T>().SingleOrDefaultAsync(item => item.Id == id);
                return _context.Set<T>().Remove(searchedItem) != null ? true : false;
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
