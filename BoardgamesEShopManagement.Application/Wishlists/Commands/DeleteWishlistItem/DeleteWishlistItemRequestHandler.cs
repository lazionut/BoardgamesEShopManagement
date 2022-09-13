using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Utils;

namespace BoardgamesEShopManagement.Application.Orders.Commands.DeleteWishlistItem
{
    public class DeleteWishlistItemRequestHandler : IRequestHandler<DeleteWishlistItemRequest, Wishlist?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWishlistItemRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Wishlist?> Handle(DeleteWishlistItemRequest request, CancellationToken cancellationToken)
        {
            Wishlist? searchedWishlist = await _unitOfWork.WishlistRepository.GetByAccount
                (request.WishlistAccountId, request.WishlistId);

            if (searchedWishlist == null)
            {
                return null;
            } 

            Boardgame? searchedBoardgame = await _unitOfWork.BoardgameRepository.GetById(request.WishlistBoardgameId);

            if (searchedBoardgame == null)
            {
                return null;
            }

            searchedWishlist.Boardgames.Remove(searchedBoardgame);

            searchedWishlist.UpdatedAt = DateTimeUtils.GetCurrentDateTimeWithoutMiliseconds();

            await _unitOfWork.Save();

            return searchedWishlist;
        }
    }
}
