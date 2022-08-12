using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Wishlist : EntityBase
    {
        public string WishlistName { get; set; }
        public List<WishlistItem> WishlistBoardgames = new();
    }
}
