using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgame
{
    public class GetReviewsListPerBoardgameQuery : IRequest<List<Review>>
    {
        public int ReviewBoardgameId { get; set; }
        public int ReviewPageIndex { get; set; }
        public int ReviewPageSize { get; set; }
    }
}