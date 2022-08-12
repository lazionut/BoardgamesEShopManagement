using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Exceptions;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        protected readonly List<T> genericItems = new();

        public void Create(T item)
        {
            if (item == null)
                throw new GenericItemException($"{item} can\'t be created!");

            genericItems.Add(item);
            item.Id = genericItems.Count;
        }

        public IEnumerable<T> GetAll()
        {
            return genericItems;
        }

        public T GetById(int id)
        {
            if (id >= 0)
            {
                return genericItems.FirstOrDefault(item => item.Id == id);
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public bool Delete(int id)
        {
            if (id >= 0)
            {
                T searchedItem = genericItems.FirstOrDefault(item => item.Id == id);
                return genericItems.Remove(searchedItem);
            }
            else
            {
                throw new NegativeIdException();
            }
        }
    }
}
