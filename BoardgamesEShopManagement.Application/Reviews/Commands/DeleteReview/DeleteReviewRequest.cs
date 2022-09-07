using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewRequest : IRequest<Review>
    {
        public int ReviewId { get; set; }
    }
}
