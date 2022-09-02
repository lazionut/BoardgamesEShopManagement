using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsList
{
    public class GetReviewsListQueryHandler : IRequestHandler<GetReviewsListQuery, List<Review>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewsListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Review>> Handle(GetReviewsListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ReviewRepository.GetAll(request.ReviewPageIndex, request.ReviewPageSize);
        }
    }
}
