using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewRequest : IRequest<Review>
    {
        public string ReviewTitle { get; set; } = null!;
        public byte ReviewScore { get; set; }
        public string ReviewContent { get; set; } = null!;
        public int ReviewBoardgameId { get; set; }
        public int ReviewAccountId { get; set; }
    }
}