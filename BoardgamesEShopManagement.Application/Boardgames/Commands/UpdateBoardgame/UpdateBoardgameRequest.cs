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
        public string? Image { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Link { get; set; }
        public int CategoryId { get; set; }
    }
}
