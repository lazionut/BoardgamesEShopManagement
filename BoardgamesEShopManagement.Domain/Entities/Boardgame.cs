using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Exceptions;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Boardgame : EntityBase, IComparable<Boardgame>
    {
        public Boardgame()
        {

        }

        public Boardgame(int boardgameId, string boardgameImage, string boardgameName, string boardgameDescription, decimal boardgamePrice)
        {
            Id = boardgameId;
            BoardgameImage = boardgameImage;
            BoardgameName = boardgameName;
            BoardgameDescription = boardgameDescription;
            BoardgamePrice = boardgamePrice;
        }


        public string BoardgameImage { get; set; }
        public string BoardgameName { get; set; }
        public string BoardgameDescription { get; set; }
        public decimal BoardgamePrice { get; set; }

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
