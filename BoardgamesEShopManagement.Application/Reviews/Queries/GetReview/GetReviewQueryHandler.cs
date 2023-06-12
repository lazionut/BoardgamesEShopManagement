using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReview
{
    public class GetReviewQueryHandler : IRequestHandler<GetReviewQuery, Review?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review?> Handle(GetReviewQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ReviewRepository.GetById(request.ReviewId);
        }
    }
}