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
    public class GetWishlistListPerAccountQueryHandler : IRequestHandler<GetWishlistListPerAccountQuery, List<Wishlist>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWishlistListPerAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Wishlist>> Handle(GetWishlistListPerAccountQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WishlistRepository.GetWishlistsListPerAccount(request.AccountId);
        }
    }
}
