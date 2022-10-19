using MediatR;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlistItem
{
    public class DeleteWishlistItemRequest : IRequest<bool>
    {
        public int WishlistId { get; set; }
        public int BoardgameId { get; set; }
    }
}