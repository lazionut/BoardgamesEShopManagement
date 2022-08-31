using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<Review> _logger;

        public ReviewRepository(ShopContext context, ILogger<Review> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Review>> GetReviewsListPerBoardgame(int boardgameId)
        {
            _logger.LogInformation("Getting the list of reviews per desired boardgame...");
            return await _context.Reviews
                .Where(review => review.BoardgameId == boardgameId)
                .ToListAsync();
        }
    }
}
