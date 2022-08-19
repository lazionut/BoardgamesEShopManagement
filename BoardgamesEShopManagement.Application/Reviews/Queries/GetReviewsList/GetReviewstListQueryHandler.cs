using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsList
{
    public class GetReviewsListQueryHandler : IRequestHandler<GetReviewsListQuery, IEnumerable<ReviewListVm>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewsListQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task<IEnumerable<ReviewListVm>> Handle(GetReviewsListQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<ReviewListVm> result = _reviewRepository.GetAll().Select(review => new ReviewListVm
            {
                BoardgameId = review.BoardgameId,
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
