using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame
{
    public class DeleteBoardgameRequest : IRequest<Boardgame>
    {
        public int BoardgameId { get; set; }
    }
}