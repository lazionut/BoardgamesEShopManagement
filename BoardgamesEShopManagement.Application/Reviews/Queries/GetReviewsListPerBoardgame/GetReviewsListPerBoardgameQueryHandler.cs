using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgame
{
    public class GetReviewsListPerBoardgameQueryHandler : IRequestHandler<GetReviewsListPerBoardgameQuery, IEnumerable<ReviewsListPerBoardgameVm>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewsListPerBoardgameQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task<IEnumerable<ReviewsListPerBoardgameVm>> Handle(GetReviewsListPerBoardgameQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ReviewsListPerBoardgameVm> result = _reviewRepository.GetReviewsListPerBoardgame(request.BoardgameId).Select(review => new ReviewsListPerBoardgameVm
            {
                ReviewId = review.Id,
                ReviewTitle = review.Title,
                ReviewAuthor = review.Author,
                ReviewScore = review.Score,
                ReviewContent = review.Content
            });

            return Task.FromResult(result);
        }
    }
}
