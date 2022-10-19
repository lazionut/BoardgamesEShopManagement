using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.UpdateWishlist
{
    public class UpdateWishlistRequestHandler : IRequestHandler<UpdateWishlistRequest, Wishlist?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWishlistRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Wishlist?> Handle(UpdateWishlistRequest request, CancellationToken cancellationToken)
        {

            Wishlist? wishlist = await _unitOfWork.WishlistRepository.GetByAccount
                (request.WishlistAccountId, request.WishlistId);

            if (wishlist == null)
            {
                return null;
            }

            foreach (int boardgameId in request.WishlistBoardgameIds)
            {
                Boardgame? boardgame = await _unitOfWork.BoardgameRepository.GetById(boardgameId);

                if (boardgame == null)
                {
                    return null;
                }

                await _unitOfWork.WishlistRepository.CreateItem(wishlist.AccountId, wishlist.Id, boardgameId, wishlist);
            }

            wishlist.Name = request.WishlistName;
            wishlist.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Save();

            return wishlist;
        }

    }
}
