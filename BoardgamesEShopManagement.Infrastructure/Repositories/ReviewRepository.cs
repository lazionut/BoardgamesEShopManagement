using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<ReviewRepository> _logger;

        public ReviewRepository(ShopContext context, ILogger<ReviewRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Review>?> GetPerBoardgame(int boardgameId, int pageIndex, int pageSize)
        {
            _logger.LogInformation("Reading {Review} with boardgame id {BoardgameId}", typeof(Review).Name, boardgameId);
            return await _context.Reviews
                .Where(review => review.BoardgameId == boardgameId)
                .OrderByDescending(review => review.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetPerBoardgameCounter(int boardgameId)
        {
            _logger.LogInformation("Reading number of {Review} with boardgame id {BoardgameId}", typeof(Review).Name, boardgameId);
            return await _context.Reviews.Select(review => review.BoardgameId == boardgameId).CountAsync();
        }

        public async Task<bool> IsBoardgameReviewed(int accountId, int boardgameId)
        {
            _logger.LogInformation("Reading {Review} of boardgame id {BoardgameId} for account id {AccountId}", typeof(Review).Name, boardgameId, accountId);
            Review? searchedReview = await _context.Reviews.SingleOrDefaultAsync(review => review.AccountId == accountId && review.BoardgameId == boardgameId);

            if (searchedReview == null)
            {
                return false;
            }

            return true;
        }
    }
}