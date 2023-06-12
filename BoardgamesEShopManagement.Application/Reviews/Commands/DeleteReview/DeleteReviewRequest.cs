using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewRequest : IRequest<Review>
    {
        public int ReviewId { get; set; }
    }
}