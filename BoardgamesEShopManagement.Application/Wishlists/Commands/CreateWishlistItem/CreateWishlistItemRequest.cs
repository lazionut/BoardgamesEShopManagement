using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlistItem
{
    public class CreateWishlistItemRequest : IRequest<Wishlist>
    {
        public int WishlistAccountId { get; set; }
        public int WishlistId { get; set; }
        public int WishlistBoardgameId { get; set; }
    }
}
