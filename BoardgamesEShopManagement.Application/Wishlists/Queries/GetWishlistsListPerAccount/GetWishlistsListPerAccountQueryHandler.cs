using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistsListPerAccount
{
    public class GetWishlistsListPerAccountQueryHandler : IRequestHandler<GetWishlistsListPerAccountQuery, List<Wishlist>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWishlistsListPerAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Wishlist>?> Handle(GetWishlistsListPerAccountQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WishlistRepository.GetPerAccount(request.WishlistAccountId);
        }
    }
}
