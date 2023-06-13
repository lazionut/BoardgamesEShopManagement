using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByName
{
    public class GetBoardgamesListByNameQuery : IRequest<List<Boardgame>>
    {
        public string BoardgameNameCharacters { get; set; } = null!;
        public int BoardgamePageIndex { get; set; }
        public int BoardgamePageSize { get; set; }
        public BoardgamesSortOrdersEnum BoardgameSortOrder { get; set; }
    }
}