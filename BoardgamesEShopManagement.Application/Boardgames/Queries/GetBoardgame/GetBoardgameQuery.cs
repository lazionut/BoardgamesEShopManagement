using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame
{
    public class GetBoardgameQuery : IRequest<Boardgame>
    {
        public int BoardgameId { get; set; }
    }
}