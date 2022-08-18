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
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public IEnumerable<Review> GetReviewsListPerBoardgame(int boardgameId)
        {
            if (boardgameId >= 0)
            {
                return genericItems.Where(review => review.BoardgameId == boardgameId).ToList();
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public Review UpdateReview(int reviewId, Review review)
        {
            if (reviewId >= 0)
            {
                Review searchedReview = genericItems.FirstOrDefault(review => review.Id == reviewId);
                searchedReview.ReviewTitle = review.ReviewTitle ?? searchedReview.ReviewTitle;
                searchedReview.ReviewContent = review.ReviewContent ?? searchedReview.ReviewContent;

                return searchedReview;
            }
            else
            {
                throw new NegativeIdException();
            }
        }
    }
}
