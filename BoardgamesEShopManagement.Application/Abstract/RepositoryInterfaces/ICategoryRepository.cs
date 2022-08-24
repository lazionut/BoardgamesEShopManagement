using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> UpdateCategory(int categoryId, Category category);
    }
}
