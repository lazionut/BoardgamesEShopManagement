using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlistItem;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Wishlists
{
    public class DeleteWishlistItemRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly DeleteWishlistItemRequestHandler _handler;

        public DeleteWishlistItemRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new DeleteWishlistItemRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenWishlistDoesNotExist()
        {
            // Arrange
            var request = new DeleteWishlistItemRequest
            {
                WishlistId = 1,
                BoardgameId = 1
            };

            _unitOfWorkMock.WishlistRepository.GetById(request.WishlistId).Returns((Wishlist?)null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenBoardgameDoesNotExist()
        {
            // Arrange
            var request = new DeleteWishlistItemRequest
            {
                WishlistId = 1,
                BoardgameId = 1
            };

            var wishlist = new Wishlist { Id = 1, Name = "My Wishlist", Boardgames = new List<Boardgame>() };

            _unitOfWorkMock.WishlistRepository.GetById(request.WishlistId).Returns(wishlist);
            _unitOfWorkMock.BoardgameRepository.GetById(request.BoardgameId).Returns((Boardgame?)null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenWishlistItemIsDeleted()
        {
            // Arrange
            var request = new DeleteWishlistItemRequest
            {
                WishlistId = 1,
                BoardgameId = 1
            };

            var boardgame = new Boardgame { Id = 1, Name = "Chess" };
            var wishlist = new Wishlist { Id = 1, Name = "My Wishlist", Boardgames = new List<Boardgame> { boardgame } };

            _unitOfWorkMock.WishlistRepository.GetById(request.WishlistId).Returns(wishlist);
            _unitOfWorkMock.BoardgameRepository.GetById(request.BoardgameId).Returns(boardgame);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(boardgame, wishlist.Boardgames);

            await _unitOfWorkMock.Received(1).Save();
        }
    }
}