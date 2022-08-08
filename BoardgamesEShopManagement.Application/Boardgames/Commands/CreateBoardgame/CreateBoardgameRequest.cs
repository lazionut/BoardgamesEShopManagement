using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame
{
    public class CreateBoardgameRequest : IRequest<int>
    {
        public int BoardgameId { get; set; }
        public string BoardgameImage { get; set; } = null!;
        public string BoardgameName { get; set; } = null!;
        public string BoardgameDescription { get; set; } = null!;
        public decimal BoardgamePrice { get; set; }
    }
}
