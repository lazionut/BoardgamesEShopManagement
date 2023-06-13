using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

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