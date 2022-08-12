using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.RepositoryInterfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Category Update(int categoryId, Category category);
    }
}
