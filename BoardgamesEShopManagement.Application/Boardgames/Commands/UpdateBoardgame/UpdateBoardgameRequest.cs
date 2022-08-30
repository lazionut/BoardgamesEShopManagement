using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.UpdateBoardgame
{
    public class UpdateBoardgameRequest : IRequest<Boardgame>
    {
        public int BoardgameId { get; set; }
        public string? BoardgameImage { get; set; }
        public string BoardgameName { get; set; } = null!;
        public string? BoardgameDescription { get; set; }
        public decimal BoardgamePrice { get; set; }
        public string? BoardgameLink { get; set; }
        public int BoardgameQuantity { get; set; }
        public int BoardgameCategoryId { get; set; }
    }
}
