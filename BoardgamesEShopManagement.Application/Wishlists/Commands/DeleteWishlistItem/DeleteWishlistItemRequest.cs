using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Orders.Commands.DeleteWishlistItem
{
    public class DeleteWishlistItemRequest : IRequest<bool>
    {
        public int WishlistAccountId { get; set; }
        public int WishlistId { get; set; }
        public int WishlistBoardgameId { get; set; }
    }
}
