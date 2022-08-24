using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ShopContext _context;

        public CategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> UpdateCategory(int categoryId, Category category)
        {
            if (categoryId >= 0)
            {
                Category searchedCategory = await _context.Categories.SingleOrDefaultAsync(category => category.Id == categoryId);
                searchedCategory.Name = category.Name ?? searchedCategory.Name;

                _context.Update(searchedCategory);

                return searchedCategory;
            }
            else
            {
                throw new NegativeIdException();
            }
        }
    }
}
