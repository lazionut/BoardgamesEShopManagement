using MediatR;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgameCounter
{
    public class GetReviewsListPerBoardgameCounterQuery : IRequest<int>
    {
        public int ReviewBoardgameId { get; set; }
    }
}