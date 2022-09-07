using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame
{
    public class DeleteBoardgameRequest : IRequest<Boardgame>
    {
        public int BoardgameId { get; set; }
    }
}
