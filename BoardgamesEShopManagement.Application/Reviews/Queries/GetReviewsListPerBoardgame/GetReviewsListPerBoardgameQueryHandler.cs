using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgame
{
    public class GetReviewsListPerBoardgameQueryHandler : IRequestHandler<GetReviewsListPerBoardgameQuery, List<Review>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewsListPerBoardgameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Review>?> Handle(GetReviewsListPerBoardgameQuery request, CancellationToken cancellationToken)
        {
            Review? searchedReview = await _unitOfWork.ReviewRepository.GetById(request.ReviewBoardgameId);

            if (searchedReview == null)
            {
                return null;
            }

            return await _unitOfWork.ReviewRepository.GetReviewsListPerBoardgame
                (request.ReviewBoardgameId, request.ReviewPageIndex, request.ReviewPageSize);
        }
    }
}
