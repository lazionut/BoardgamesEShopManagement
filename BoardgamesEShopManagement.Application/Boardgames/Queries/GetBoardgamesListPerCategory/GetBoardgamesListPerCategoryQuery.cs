using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategory
{
    public class GetBoardgamesListPerCategoryQuery : IRequest<List<Boardgame>>
    {
        public int CategoryId { get; set; }
        public int BoardgamePageIndex { get; set; }
        public int BoardgamePageSize { get; set; }
        public BoardgamesSortOrdersMode BoardgameSortOrder { get; set; }
    }
}