using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList
{
    public class BoardgamesListVm
    {
        public int Id { get; set; }
        public string BoardgameImage { get; set; } = null!;
        public string BoardgameName { get; set; } = null!;
        public string BoardgameDescription { get; set; } = null!;
        public decimal BoardgamePrice { get; set; }
        public string BoardgameLink { get; set; } = null!;
    }
}
