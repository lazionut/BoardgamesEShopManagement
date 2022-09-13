using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Orders.Commands.DeleteWishlistItem
{
    public class DeleteWishlistItemRequest : IRequest<Wishlist>
    {
        public int WishlistAccountId { get; set; }
        public int WishlistId { get; set; }
        public int WishlistBoardgameId { get; set; }
    }
}
