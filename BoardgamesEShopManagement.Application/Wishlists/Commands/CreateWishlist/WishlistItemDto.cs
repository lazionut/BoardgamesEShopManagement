using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist
{
    public class WishlistItemDto
    {
        public string BoardgameName { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
