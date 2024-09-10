using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlist;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Queries.Wishlists
{
    public class GetWishlistQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetWishlistQueryHandler _handler;

        public GetWishlistQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetWishlistQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnWishlist_WhenWishlistExists()
        {
            // Arrange
            var wishlistId = 1;
            var wishlist = new Wishlist { Id = wishlistId };
            _unitOfWorkMock.WishlistRepository.GetById(wishlistId).Returns(Task.FromResult<Wishlist?>(wishlist));
            var query = new GetWishlistQuery { WishlistId = wishlistId };

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(wishlistId, result.Id);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenWishlistDoesNotExist()
        {
            // Arrange
            var wishlistId = 1;
            _unitOfWorkMock.WishlistRepository.GetById(wishlistId).Returns(Task.FromResult<Wishlist?>(null));
            var query = new GetWishlistQuery { WishlistId = wishlistId };

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.Null(result);
        }
    }
}