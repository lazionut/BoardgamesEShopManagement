using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlist
{
    public class GetWishlistQueryHandler : IRequestHandler<GetWishlistQuery, Wishlist?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWishlistQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Wishlist?> Handle(GetWishlistQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WishlistRepository.GetById(request.WishlistId);
        }
    }
}
