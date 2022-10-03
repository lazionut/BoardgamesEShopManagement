using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategoryCounter
{
    public class GetBoardgamesListPerCategoryCounterQuery : IRequest<int>
    {
        public int CategoryId { get; set; }
    }
}