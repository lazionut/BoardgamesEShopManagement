using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistList
{
    public class OrderListVm
    {
        public int OrderId { get; set; }
        public string BuyerName { get; set; } = null!;
        public List<OrderItemListDto> OrderItems { get; set; } = null!;
    }
}
