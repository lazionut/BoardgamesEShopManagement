using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistsListPerAccount
{
    public class GetWishlistsListPerAccountQuery : IRequest<List<Wishlist>>
    {
        public int WishlistAccountId { get; set; }

    }
}