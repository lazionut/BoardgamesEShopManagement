using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist
{
    public class DeleteWishlistRequest : IRequest<Wishlist>
    {
        public int WishlistId { get; set; }
        public int WishlistAccountId { get; set; }
    }
}