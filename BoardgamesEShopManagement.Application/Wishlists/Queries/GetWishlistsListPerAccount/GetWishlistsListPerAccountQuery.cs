using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistsListPerAccount
{
    public class GetWishlistsListPerAccountQuery : IRequest<List<Wishlist>>
    {
        public int WishlistAccountId { get; set; }
    }
}