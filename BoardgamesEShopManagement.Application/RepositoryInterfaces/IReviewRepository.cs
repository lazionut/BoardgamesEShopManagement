using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.RepositoryInterfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        public IEnumerable<Review> GetReviewsListperBoardgame(int boardgameId);
        Review Update(int reviewId, Review review);
    }
}
