using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveBoardgame
{
    public class ArchiveBoardgameRequest : IRequest<Boardgame>
    {
        public int BoardgameId { get; set; }
    }
}