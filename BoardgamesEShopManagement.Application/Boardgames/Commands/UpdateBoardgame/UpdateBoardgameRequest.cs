using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.UpdateBoardgame
{
    public class UpdateBoardgameRequest : IRequest<Boardgame>
    {
        public int BoardgameId { get; set; }
        public Boardgame Boardgame { get; set; } = null!;
    }
}
