using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewRequestHandler : IRequestHandler<UpdateReviewRequest, Review>
    {
        private readonly IReviewRepository _reviewRepository;
        public UpdateReviewRequestHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public Task<Review> Handle(UpdateReviewRequest request, CancellationToken cancellationToken)
        {
            Review updatedReview = _reviewRepository.Update(request.ReviewId, request.Review);

            return Task.FromResult(updatedReview);
        }
    }
}
