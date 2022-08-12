using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist
{
    public class WishlistBoardgameDto
    {
        public string BoardgameName { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
