using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveBoardgame
{
    public class ArchiveBoardgameRequest : IRequest<Boardgame>
    {
        public int BoardgameId { get; set; }
    }
}
