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
        public string? Image { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(4000)]
        public string? Description { get; set; }
        public decimal Price { get; set; }

        [MaxLength(2000)]
        public string? Link { get; set; }
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public ICollection<Review> Reviews { get; set; } = null!;
        public ICollection<Wishlist> Wishlists { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = null!;

        public override string ToString()
        {
            return $"{Id} -> {Name} - {Description} | ({Price})";
        }

        public int CompareTo(Boardgame boardgame)
        {
            if (this.Price > boardgame.Price)
            {
                return 1;
            }
            else if (this.Price < boardgame.Price)
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
