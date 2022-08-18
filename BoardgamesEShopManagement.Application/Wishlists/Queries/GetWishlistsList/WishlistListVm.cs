using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistList
{
    public class WishlistListVm
    {
        public int WishlistId { get; set; }
        public string WishlistName { get; set; } = null!;
        public List<WishlistItemListDto> WishlistItems { get; set; } = null!;
    }
}
