using MediatR;

using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewRequestHandler : IRequestHandler<DeleteReviewRequest, Review>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review> Handle(DeleteReviewRequest request, CancellationToken cancellationToken)
        {
            Review deletedReview = await _unitOfWork.ReviewRepository.Delete(request.ReviewId);

            if (deletedReview == null)
            {
                return null;
            }

            await _unitOfWork.Save();

            return deletedReview;
        }
    }
}
