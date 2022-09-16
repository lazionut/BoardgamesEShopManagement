using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewRequestHandler : IRequestHandler<UpdateReviewRequest, Review?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReviewRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review?> Handle(UpdateReviewRequest request, CancellationToken cancellationToken)
        {
            Review? updatedReview = await _unitOfWork.ReviewRepository.GetById(request.ReviewId);

            if (updatedReview == null)
            {
                return null;
            }

            updatedReview.Title = request.ReviewTitle ?? updatedReview.Title;
            updatedReview.Content = request.ReviewContent ?? updatedReview.Content;

            updatedReview.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.ReviewRepository.Update(updatedReview);
            await _unitOfWork.Save();

            return updatedReview;
        }
    }
}
