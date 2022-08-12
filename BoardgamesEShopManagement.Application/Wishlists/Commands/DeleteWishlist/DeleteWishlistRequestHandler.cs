using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist
{
    public class DeleteWishlistRequestHandler : IRequestHandler<DeleteWishlistRequest, bool>
    {
        private readonly IWishlistRepository _wishlistRepository;
        public DeleteWishlistRequestHandler(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }
        public Task<bool> Handle(DeleteWishlistRequest request, CancellationToken cancellationToken)
        {
            bool isDeleted = _wishlistRepository.DeleteWishlist(request.WishlistId);

            return Task.FromResult(isDeleted);
        }
    }
}
