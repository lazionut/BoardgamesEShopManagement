using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame
{
    public class DeleteBoardgameRequest : IRequest<Boardgame>
    {
        public int BoardgameId { get; set; }
    }
}
