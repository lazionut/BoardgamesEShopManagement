using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveBoardgame
{
    public class ArchiveBoardgameRequest : IRequest<Boardgame>
    {
        public int BoardgameId { get; set; }
    }
}
