using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<Category> _logger;

        public CategoryRepository(ShopContext context, ILogger<Category> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> GetCategoryCounter()
        {
            _logger.LogInformation("Getting the total number of category entries...");
            int count = _context.Categories.Count();
            return count;
        }

    }
}
