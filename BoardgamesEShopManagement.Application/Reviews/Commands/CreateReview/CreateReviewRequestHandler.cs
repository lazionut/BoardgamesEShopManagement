using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview
{
    internal class CreateReviewRequestHandler : IRequestHandler<CreateReviewRequest, int>
    {
        private readonly IReviewRepository _reviewRepository;

        public CreateReviewRequestHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task<int> Handle(CreateReviewRequest request, CancellationToken cancellationToken)
        {
            Review review = new Review
            {
                BoardgameId = request.BoardgameId,
                Title = request.ReviewTitle,
                Author = request.ReviewAuthor,
                Score = request.ReviewScore,
                Content = request.ReviewContent
            };
            _reviewRepository.Create(review);

            return Task.FromResult(review.Id);
        }
    }
}
