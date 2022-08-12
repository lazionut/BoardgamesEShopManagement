using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlist
{
    public class GetWishlistQueryHandler : IRequestHandler<GetWishlistQuery, Wishlist>
    {
        private readonly IWishlistRepository _wishlistRepository;

        public GetWishlistQueryHandler(IWishlistRepository wishlistRepository)
        {
            wishlistRepository = _wishlistRepository;
        }

        public Task<Wishlist> Handle(GetWishlistQuery request, CancellationToken cancellationToken)
        {
            Wishlist result = _wishlistRepository.GetWishlistById(request.WishlistId);

            return Task.FromResult(result);
        }
    }
}
