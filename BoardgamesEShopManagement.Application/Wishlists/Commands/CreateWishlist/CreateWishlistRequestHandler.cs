using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist
{
    public class CreateWishlistCommandHandler : IRequestHandler<CreateWishlistRequest, int>
    {
        private readonly IWishlistRepository _wishlistRepository;

        public CreateWishlistCommandHandler(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public Task<int> Handle(CreateWishlistRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<WishlistItem> wishlistBoardgames = request.WishlistBoardgames.Select(wishlistBoardgameDto =>
                new WishlistItem { Boardgame = new Boardgame { BoardgameName = wishlistBoardgameDto.BoardgameName }, Quantity = wishlistBoardgameDto.Quantity });

            Wishlist wishlist = new Wishlist { WishlistName = request.WishlistName, WishlistBoardgames = wishlistBoardgames.ToList() };
            _wishlistRepository.CreateWishlist(wishlist);

            return Task.FromResult(wishlist.Id);
        }
    }
}
