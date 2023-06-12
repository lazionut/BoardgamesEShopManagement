using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.UpdateWishlist
{
    public class UpdateWishlistRequest : IRequest<Wishlist>
    {
        public int WishlistId { get; set; }
        public string WishlistName { get; set; } = null!;
        public int WishlistAccountId { get; set; }
        public List<int> WishlistBoardgameIds { get; set; } = null!;
    }
}