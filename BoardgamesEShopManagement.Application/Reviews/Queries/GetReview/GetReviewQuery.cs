using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReview
{
    public class GetReviewQuery : IRequest<Review>
    {
        public int ReviewId { get; set; }
    }
}