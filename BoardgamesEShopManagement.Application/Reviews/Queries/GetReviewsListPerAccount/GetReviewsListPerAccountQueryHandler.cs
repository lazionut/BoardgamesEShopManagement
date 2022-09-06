using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerAccount
{
    public class GetReviewsListPerAccountQueryHandler : IRequestHandler<GetReviewsListPerAccountQuery, List<Review>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewsListPerAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Review>?> Handle(GetReviewsListPerAccountQuery request, CancellationToken cancellationToken)
        {
            Review? searchedReview = await _unitOfWork.ReviewRepository.GetById(request.ReviewAccountId);

            if (searchedReview == null)
            {
                return null;
            }

            return await _unitOfWork.ReviewRepository.GetReviewsListPerAccount
                (request.ReviewAccountId, request.ReviewPageIndex, request.ReviewPageSize);
        }
    }
}
