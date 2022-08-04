
using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Exceptions;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : EntityBase, new()
    {
        public List<T> genericItems = new List<T>();

        public void Create(T item)
        {
            if (item == null)
                throw new GenericItemException($"{item} can\'t be created!");

            genericItems.Add(item);
        }

        public void GetAll()
        {
            foreach (T item in genericItems)
            {
                Console.WriteLine(item);
            }
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

        public void Update(int id)
        {
            if (id >= 0)
            {
                T searchedItem = genericItems.FirstOrDefault(item => item.Id == id);
                searchedItem = new T();
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public void Delete(int id)
        {
            if (id >= 0)
            {
                T searchedItem = genericItems.FirstOrDefault(item => item.Id == id);
                genericItems.Remove(searchedItem);
            }
            else
            {
                throw new NegativeIdException();
            }
        }
    }
}
