using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Exceptions;

namespace BoardgamesEShopManagement.Domain.Models
{
    public class Boardgame
    {
        public int BoardgameId { get; set; }
        public string BoardgameImage { get; set; }
        public string BoardgameName { get; set; }
        public string BoardgameDescription { get; set; }
        public decimal BoardgamePrice { get; set; }

        public static List<Boardgame> Boardgames = new List<Boardgame>();


        public override string ToString()
        {
            return $"{BoardgameId} - {BoardgameName} -> {BoardgameDescription} - ({BoardgamePrice}))";
        }
    }
}
