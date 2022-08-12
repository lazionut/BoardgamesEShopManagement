using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Boardgame : EntityBase, IComparable<Boardgame>
    {
        public int CategoryId { get; set; }
        public string BoardgameImage { get; set; }
        public string BoardgameName { get; set; }
        public string BoardgameDescription { get; set; }
        public decimal BoardgamePrice { get; set; }
        public string BoardgameLink { get; set; }

        public override string ToString()
        {
            return $"{Id} -> {BoardgameName} - {BoardgameDescription} | ({BoardgamePrice})";
        }

        public int CompareTo(Boardgame boardgame)
        {
            if (this.BoardgamePrice > boardgame.BoardgamePrice)
            {
                return 1;
            }
            else if (this.BoardgamePrice < boardgame.BoardgamePrice)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
