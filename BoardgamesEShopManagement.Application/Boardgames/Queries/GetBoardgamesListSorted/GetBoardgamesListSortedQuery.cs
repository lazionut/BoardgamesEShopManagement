using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListSorted
{
    public class GetBoardgamesListSortedQuery : IRequest<List<Boardgame>>
    {
        public int BoardgamePageIndex { get; set; }
        public int BoardgamePageSize { get; set; }
        public BoardgamesSortOrdersEnum BoardgameSortOrder { get; set; }
    }
}