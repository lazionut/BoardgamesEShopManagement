using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task Create(T item);

        Task<List<T>> GetAll();

        Task<T> GetById(int id);

        Task<bool> Delete(int id);

        Task Save();
    }
}
