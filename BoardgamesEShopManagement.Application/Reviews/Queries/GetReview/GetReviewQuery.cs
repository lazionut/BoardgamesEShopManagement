using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReview
{
    public class GetReviewQuery : IRequest<Review>
    {
        public int ReviewId { get; set; }
    }
}