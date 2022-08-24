using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist
{
    public class DeleteWishlistRequestHandler : IRequestHandler<DeleteWishlistRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWishlistRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteWishlistRequest request, CancellationToken cancellationToken)
        {
            bool isWishlistDeleted = await _unitOfWork.WishlistRepository.Delete(request.WishlistId);

            await _unitOfWork.Save();

            return isWishlistDeleted;
        }
    }
}
