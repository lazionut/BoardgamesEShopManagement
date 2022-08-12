using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Exceptions;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public Category Update(int id, Category category)
        {
            if (id >= 0)
            {
                Category searchedCategory = genericItems.FirstOrDefault(category => category.Id == id);
                searchedCategory.CategoryName = category.CategoryName ?? searchedCategory.CategoryName;

                return searchedCategory;
            }
            else
            {
                throw new NegativeIdException();
            }
        }
    }
}
