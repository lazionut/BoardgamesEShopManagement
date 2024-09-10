using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Wishlists
{
    public class DeleteWishlistRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly DeleteWishlistRequestHandler _handler;

        public DeleteWishlistRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new DeleteWishlistRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnDeletedWishlist_WhenWishlistExists()
        {
            // Arrange
            var accountId = 1;
            var wishlistId = 1;
            var expectedWishlist = new Wishlist { Id = wishlistId, AccountId = accountId, Name = "My Wishlist" };

            _unitOfWorkMock.WishlistRepository.Delete(accountId, wishlistId).Returns(Task.FromResult<Wishlist?>(expectedWishlist));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            var command = new DeleteWishlistRequest { WishlistAccountId = accountId, WishlistId = wishlistId };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedWishlist.Id, actualResult?.Id);
            Assert.Equal(expectedWishlist.AccountId, actualResult?.AccountId);
            Assert.Equal(expectedWishlist.Name, actualResult?.Name);

            await _unitOfWorkMock.WishlistRepository.Received(1).Delete(accountId, wishlistId);
            await _unitOfWorkMock.Received(1).Save();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenWishlistDoesNotExist()
        {
            // Arrange
            var accountId = 1;
            var wishlistId = 1;

            _unitOfWorkMock.WishlistRepository.Delete(accountId, wishlistId).Returns(Task.FromResult<Wishlist?>(null));

            var command = new DeleteWishlistRequest { WishlistAccountId = accountId, WishlistId = wishlistId };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.WishlistRepository.Received(1).Delete(accountId, wishlistId);
            await _unitOfWorkMock.DidNotReceive().Save();
        }
    }
}