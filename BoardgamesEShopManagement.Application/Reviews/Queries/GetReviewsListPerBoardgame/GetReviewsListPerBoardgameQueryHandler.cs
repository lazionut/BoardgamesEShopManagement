﻿using MediatR;

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
            return await _unitOfWork.ReviewRepository.GetPerBoardgame(request.ReviewBoardgameId, request.ReviewPageIndex, request.ReviewPageSize);
        }
    }
}
