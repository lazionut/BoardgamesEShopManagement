using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetWishlistsListPerAccount
{
    public class GetWishlistsListPerAccountQueryHandler : IRequestHandler<GetWishlistsListPerAccountQuery, List<Wishlist>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWishlistsListPerAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Wishlist>> Handle(GetWishlistsListPerAccountQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WishlistRepository.GetWishlistsListPerAccount
                (request.WishlistAccountId, request.WishlistOffset, request.WishlistLimit);
        }
    }
}
