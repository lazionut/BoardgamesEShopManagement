using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistByAccount
{
    public class GetWishlistByAccountQueryHandler : IRequestHandler<GetWishlistByAccountQuery, Wishlist?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWishlistByAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Wishlist?> Handle(GetWishlistByAccountQuery request, CancellationToken cancellationToken)
        {
            Wishlist? wishlistByAccount =  await _unitOfWork.WishlistRepository.GetByAccount(request.WishlistAccountId, request.WishlistId);

            if (wishlistByAccount == null)
            {
                return null;
            }

            return wishlistByAccount;
        }
    }
}
