using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame
{
    public class DeleteBoardgameRequest : IRequest<bool>
    {
        public int BoardgameId { get; set; }
    }
}
