using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Boardgame : EntityBase, IComparable<Boardgame>
    {
        public string? BoardgameImage { get; set; }

        [MaxLength(50)]
        public string BoardgameName { get; set; } = null!;

        [MaxLength(4000)]
        public string? BoardgameDescription { get; set; }
        public decimal BoardgamePrice { get; set; }

        [MaxLength(2000)]
        public string? BoardgameLink { get; set; }
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public ICollection<Review> Reviews { get; set; } = null!;
        public ICollection<Wishlist> Wishlists { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = null!;

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
