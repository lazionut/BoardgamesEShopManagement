using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist
{
    public class CreateWishlistCommandHandler : IRequestHandler<CreateWishlistRequest, Wishlist>
    {

        private readonly IUnitOfWork _unitOfWork;

        public CreateWishlistCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Wishlist> Handle(CreateWishlistRequest request, CancellationToken cancellationToken)
        {
            Boardgame boardgame = await _unitOfWork.BoardgameRepository.GetById(request.BoardgameId);
            Wishlist wishlist = await _unitOfWork.WishlistRepository.GetById(request.WishlistId);

            if (boardgame != null && wishlist != null)
            {
                //decimal boardgamePrice = await _unitOfWork.BoardgameRepository.GetBoardgamePrice(request.BoardgameId);
                //Order newOrder = new Order { Total = request.Total, AccountId = request.AccountId };

                await _unitOfWork.WishlistRepository.Create(request.WishlistId, request.BoardgameId, wishlist);
                await _unitOfWork.Save();

                return wishlist;
            }
            else
            {
                return null;
            }
        }
        
    }
}
