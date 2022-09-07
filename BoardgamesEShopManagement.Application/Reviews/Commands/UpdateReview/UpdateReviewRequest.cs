using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewRequest : IRequest<Review>
    {
        public int ReviewId { get; set; }
        public string ReviewTitle { get; set; } = null!;
        public string ReviewContent { get; set; } = null!;
    }
}
