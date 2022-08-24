using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewRequestHandler : IRequestHandler<UpdateReviewRequest, Review>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReviewRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review> Handle(UpdateReviewRequest request, CancellationToken cancellationToken)
        {
            Review updatedReview = await _unitOfWork.ReviewRepository.UpdateReview(request.ReviewId, request.Review);

            await _unitOfWork.Save();

            return updatedReview;
        }
    }
}
