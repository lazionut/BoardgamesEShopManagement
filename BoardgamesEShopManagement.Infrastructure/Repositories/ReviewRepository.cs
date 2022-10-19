using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;

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

        public async Task<List<Review>?> GetPerBoardgame(int boardgameId, int pageIndex, int pageSize)
        {
            _logger.LogInformation("Getting the list of reviews by the boardgame identifier...");
            return await _context.Reviews
                .Where(review => review.BoardgameId == boardgameId)
                .OrderByDescending(review => review.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetPerBoardgameCounter(int boardgameId)
        {
            _logger.LogInformation("Getting the total number of review entries of a boardgame...");
            return await _context.Reviews.Select(review => review.BoardgameId == boardgameId).CountAsync();
        }

        public async Task<bool> IsBoardgameReviewed(int accountId, int boardgameId)
        {
            _logger.LogInformation("Getting the first review by account and boardgame identifiers...");
            Review? searchedReview =  await _context.Reviews.FirstOrDefaultAsync(review => review.AccountId == accountId && review.BoardgameId == boardgameId);

            if(searchedReview == null)
            {
                return false;
            }

            return true;
        }
    }
}
