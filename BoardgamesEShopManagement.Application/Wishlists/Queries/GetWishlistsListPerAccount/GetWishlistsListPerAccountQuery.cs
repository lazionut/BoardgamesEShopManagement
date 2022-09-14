using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistsListPerAccount
{
    public class GetWishlistsListPerAccountQuery : IRequest<List<Wishlist>>
    {
        public int WishlistAccountId { get; set; }
        public int WishlistPageIndex { get; set; }
        public int WishlistPageSize { get; set; }

    }
}