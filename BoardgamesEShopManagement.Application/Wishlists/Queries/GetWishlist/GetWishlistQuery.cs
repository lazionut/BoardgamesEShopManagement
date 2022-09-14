using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlist
{
    public class GetWishlistQuery : IRequest<Wishlist>
    {
        public int WishlistId { get; set; }
    }
}