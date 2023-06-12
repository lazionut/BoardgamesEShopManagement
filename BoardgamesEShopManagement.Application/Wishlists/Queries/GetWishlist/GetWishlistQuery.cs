using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlist
{
    public class GetWishlistQuery : IRequest<Wishlist>
    {
        public int WishlistId { get; set; }
    }
}