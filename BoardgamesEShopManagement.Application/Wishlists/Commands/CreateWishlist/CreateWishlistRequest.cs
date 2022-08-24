using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist
{
    public class CreateWishlistRequest : IRequest<Wishlist>
    {
        public string WishlistName { get; set; } = null!;
        public int AccountId { get; set; }
        public int BoardgameId { get; set; }
        public int WishlistId { get; set; }
    }
}
