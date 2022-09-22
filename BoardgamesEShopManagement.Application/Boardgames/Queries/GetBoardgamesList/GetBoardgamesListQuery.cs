using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList
{
    public class GetBoardgamesListQuery : IRequest<List<Boardgame>>
    {
        public int BoardgamePageIndex { get; set; }
        public int BoardgamePageSize { get; set; }
        public int BoardgameSortOrder { get; set; }
    }
}