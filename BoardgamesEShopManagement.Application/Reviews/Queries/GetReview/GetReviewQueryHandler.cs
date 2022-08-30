using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetReview
{
    public class GetReviewQueryHandler : IRequestHandler<GetReviewQuery, Review>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review> Handle(GetReviewQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ReviewRepository.GetById(request.ReviewId);
        }
    }
}
