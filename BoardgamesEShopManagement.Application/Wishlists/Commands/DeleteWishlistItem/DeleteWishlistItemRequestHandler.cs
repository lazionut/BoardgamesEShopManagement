using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlistItem
{
    public class DeleteWishlistItemRequestHandler : IRequestHandler<DeleteWishlistItemRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWishlistItemRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteWishlistItemRequest request, CancellationToken cancellationToken)
        {
            Wishlist? searchedWishlist = await _unitOfWork.WishlistRepository.GetById(request.WishlistId);

            if (searchedWishlist == null)
            {
                return false;
            }

            Boardgame? searchedBoardgame = await _unitOfWork.BoardgameRepository.GetById(request.BoardgameId);

            if (searchedBoardgame == null)
            {
                return false;
            }

            bool isWishlistItemDeleted = searchedWishlist.Boardgames.Remove(searchedBoardgame);

            await _unitOfWork.Save();

            return isWishlistItemDeleted;
        }
    }
}