using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Utils;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlistItem
{
    public class CreateWishlistItemRequestHandler : IRequestHandler<CreateWishlistItemRequest, Wishlist?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateWishlistItemRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Wishlist?> Handle(CreateWishlistItemRequest request, CancellationToken cancellationToken)
        {

            Wishlist? wishlist = await _unitOfWork.WishlistRepository.GetByAccount
                (request.WishlistAccountId, request.WishlistId);

            if (wishlist == null)
            {
                return null;
            }

            Boardgame? boardgame = await _unitOfWork.BoardgameRepository.GetById(request.WishlistBoardgameId);

            if (boardgame == null)
            {
                return null;
            }

            wishlist.UpdatedAt = DateTimeUtils.GetCurrentDateTimeWithoutMiliseconds();

            await _unitOfWork.WishlistRepository.CreateItem(wishlist.AccountId, wishlist.Id, boardgame.Id, wishlist);
            await _unitOfWork.Save();

            return wishlist;
        }

    }
}
