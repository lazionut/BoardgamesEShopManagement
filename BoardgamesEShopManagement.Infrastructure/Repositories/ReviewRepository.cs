using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<Review>?> GetReviewsListPerBoardgame(int boardgameId, int pageIndex, int pageSize)
        {
            _logger.LogInformation("Getting the list of reviews by the boardgame identifier...");
            return await _context.Reviews
                .Where(review => review.BoardgameId == boardgameId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Review>?> GetReviewsListPerAccount(int accountId, int pageIndex, int pageSize)
        {
            _logger.LogInformation("Getting the list of reviews by the account identifier...");
            return await _context.Reviews
                .Where(review => review.AccountId == accountId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Review?> GetByAccountId(int accountId)
        {
            _logger.LogInformation($"Getting the first review by it's boardgame identifier...");
            return await _context.Reviews.FirstOrDefaultAsync(review => review.AccountId == accountId);
        }

        public async Task<Review?> GetByBoardgameId(int boardgameId)
        {
            _logger.LogInformation($"Getting the first review by it's boardgame identifier...");
            return await _context.Reviews.FirstOrDefaultAsync(review => review.BoardgameId == boardgameId);
        }
    }
}
