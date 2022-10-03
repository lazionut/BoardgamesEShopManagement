using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgameCounter
{
    public class GetReviewsListPerBoardgameCounterQueryHandler : IRequestHandler<GetReviewsListPerBoardgameCounterQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewsListPerBoardgameCounterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetReviewsListPerBoardgameCounterQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ReviewRepository.GetPerBoardgameCounter(request.ReviewBoardgameId);
        }
    }
}
