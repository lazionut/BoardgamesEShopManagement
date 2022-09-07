using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Utils;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist
{
    public class CreateWishlistRequestHandler : IRequestHandler<CreateWishlistRequest, Wishlist?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateWishlistRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Wishlist?> Handle(CreateWishlistRequest request, CancellationToken cancellationToken)
        {
            Account? searchedAccount = await _unitOfWork.AccountRepository.GetById(request.WishlistAccountId);

            if (searchedAccount == null)
            {
                return null;
            }

            Wishlist wishlist = new Wishlist
            {
                Name = request.WishlistName,
                AccountId = request.WishlistAccountId,
            };

            await _unitOfWork.WishlistRepository.Create(wishlist);
            await _unitOfWork.Save();

            foreach (int boardgameId in request.WishlistBoardgameIds)
            {
                Boardgame? boardgame = await _unitOfWork.BoardgameRepository.GetById(boardgameId);

                if (boardgame == null)
                {
                    return null;
                }

                await _unitOfWork.WishlistRepository.CreateItem(wishlist.AccountId, wishlist.Id, boardgameId, wishlist);
            }

            await _unitOfWork.Save();

            return wishlist;
        }

    }
}
