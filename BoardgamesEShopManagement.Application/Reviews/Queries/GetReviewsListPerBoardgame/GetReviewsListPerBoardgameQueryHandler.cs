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
            IEnumerable<ReviewsListPerBoardgameVm> result = _reviewRepository.GetReviewsListperBoardgame(request.BoardgameId).Select(review => new ReviewsListPerBoardgameVm
            {
                ReviewId = review.Id,
                ReviewTitle = review.ReviewTitle,
                ReviewAuthor = review.ReviewAuthor,
                ReviewContent = review.ReviewContent
            });

            return Task.FromResult(result);
        }
    }
}
