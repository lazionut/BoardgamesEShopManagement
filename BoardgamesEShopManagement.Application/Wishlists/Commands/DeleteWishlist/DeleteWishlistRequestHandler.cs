using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist
{
    public class DeleteWishlistRequestHandler : IRequestHandler<DeleteWishlistRequest, Wishlist>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWishlistRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Wishlist> Handle(DeleteWishlistRequest request, CancellationToken cancellationToken)
        {
            Wishlist deletedWishlist = await _unitOfWork.WishlistRepository.Delete(request.WishlistId);

            if (deletedWishlist == null)
            {
                return null;
            }

            await _unitOfWork.Save();

            return deletedWishlist;
        }
    }
}
