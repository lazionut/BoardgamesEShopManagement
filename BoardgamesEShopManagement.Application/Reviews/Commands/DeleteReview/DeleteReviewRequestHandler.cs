using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview
{
    internal class DeleteReviewRequestHandler : IRequestHandler<DeleteReviewRequest, bool>
    {
        private readonly IReviewRepository _reviewRepository;

        public DeleteReviewRequestHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task<bool> Handle(DeleteReviewRequest request, CancellationToken cancellationToken)
        {
            bool updateReview = _reviewRepository.Delete(request.ReviewId);

            return Task.FromResult(updateReview);
        }
    }
}
