using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist
{
    public class CreateWishlistRequest : IRequest<Wishlist>
    {
        public int WishlistAccountId { get; set; }
        public string WishlistName { get; set; } = null!;
        public List<int> WishlistBoardgameIds { get; set; } = null!;
    }
}
