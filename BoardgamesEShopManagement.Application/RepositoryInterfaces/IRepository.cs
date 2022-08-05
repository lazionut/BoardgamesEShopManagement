using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.RepositoryInterfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        void GetAll();
        T GetById(int id);
        void Create(T item);
        void Update(int id);
        void Delete(int id);
    }
}
