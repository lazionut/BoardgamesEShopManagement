﻿using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist
{
    public class DeleteWishlistRequestHandler : IRequestHandler<DeleteWishlistRequest, Wishlist?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWishlistRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Wishlist?> Handle(DeleteWishlistRequest request, CancellationToken cancellationToken)
        {
            Wishlist? deletedWishlist = await _unitOfWork.WishlistRepository.Delete
                (request.WishlistAccountId, request.WishlistId);

            if (deletedWishlist == null)
            {
                return null;
            }

            await _unitOfWork.Save();

            return deletedWishlist;
        }
    }
}