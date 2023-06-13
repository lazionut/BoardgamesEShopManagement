using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            return await _context.Categories.CountAsync();
        }
    }
}