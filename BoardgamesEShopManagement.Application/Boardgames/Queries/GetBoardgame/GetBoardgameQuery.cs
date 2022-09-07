using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame
{
    public class GetBoardgameQuery : IRequest<Boardgame>
    {
        public int BoardgameId { get; set; }
    }
}