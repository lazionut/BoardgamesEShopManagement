using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Orders.Commands.DeleteWishlistItem
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
            Wishlist? searchedWishlist = await _unitOfWork.WishlistRepository.GetByAccount
                (request.WishlistAccountId, request.WishlistId);

            if (searchedWishlist == null)
            {
                return false;
            } 

            Boardgame? searchedBoardgame = await _unitOfWork.BoardgameRepository.GetById(request.WishlistBoardgameId);

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
