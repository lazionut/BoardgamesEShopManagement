using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewRequestHandler : IRequestHandler<CreateReviewRequest, Review>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateReviewRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review> Handle(CreateReviewRequest request, CancellationToken cancellationToken)
        {
            Review review = new Review
            {
                Title = request.ReviewTitle,
                Author = request.ReviewAuthor,
                Score = request.ReviewScore,
                Content = request.ReviewContent,
                BoardgameId = request.BoardgameId,
                AccountId = request.AccountId,
            };

            await _unitOfWork.ReviewRepository.Create(review);
            await _unitOfWork.Save();

            return review;
        }
    }
}
