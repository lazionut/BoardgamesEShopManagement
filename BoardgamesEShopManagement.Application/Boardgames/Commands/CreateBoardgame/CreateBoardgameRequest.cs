using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame
{
    public class CreateBoardgameRequest : IRequest<Boardgame>
    {
        public string? BoardgameImage { get; set; }
        public string BoardgameName { get; set; } = null!;
        public int BoardgameReleaseYear { get; set; }
        public string? BoardgameDescription { get; set; } = null!;
        public decimal BoardgamePrice { get; set; }
        public string? BoardgameLink { get; set; }
        public int BoardgameQuantity { get; set; }
        public int BoardgameCategoryId { get; set; }
    }
}
