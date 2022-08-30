using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly ShopContext _context;

        public ReviewRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetReviewsListPerBoardgame(int boardgameId)
        {
            return await _context.Reviews
                .Where(review => review.BoardgameId == boardgameId)
                .ToListAsync();
        }
    }
}
