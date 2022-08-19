using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistList
{
    public class GetWishlistsListQueryHandler : IRequestHandler<GetWishlistsListQuery, IEnumerable<WishlistListVm>>
    {
        private readonly IWishlistRepository _wishlistRepository;

        public GetWishlistsListQueryHandler(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public Task<IEnumerable<WishlistListVm>> Handle(GetWishlistsListQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<WishlistListVm> result = _wishlistRepository.GetWishlists().Select(wishlist => new WishlistListVm
            {
                WishlistId = wishlist.Id,
                WishlistName = wishlist.Name,
                /*WishlistItems = wishlist.WishlistItems.Select(boardgame => new WishlistItemListDto
                {
                    WishlistBoardgameId = boardgame.Id,
                    BoardgameName = boardgame.Boardgame.BoardgameName,
                    Quantity = boardgame.Quantity,
                }).ToList()*/
            });

            return Task.FromResult(result);
        }
    }
}
