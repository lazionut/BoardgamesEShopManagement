using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistList
{
    public class WishlistBoardgameListDto
    {
        public int WishlistBoardgameId { get; set; }
        public string BoardgameName { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
