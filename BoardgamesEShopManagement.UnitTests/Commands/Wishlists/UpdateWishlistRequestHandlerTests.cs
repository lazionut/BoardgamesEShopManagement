using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Wishlists.Commands.UpdateWishlist;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Wishlists
{
    public class UpdateWishlistRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly UpdateWishlistRequestHandler _handler;

        public UpdateWishlistRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new UpdateWishlistRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenWishlistDoesNotExist()
        {
            // Arrange
            var request = new UpdateWishlistRequest
            {
                WishlistId = 1,
                WishlistName = "Updated Wishlist",
                WishlistAccountId = 1,
                WishlistBoardgameIds = new List<int> { 1, 2, 3 }
            };

            _unitOfWorkMock.WishlistRepository.GetByAccount(request.WishlistAccountId, request.WishlistId).Returns((Wishlist?)null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBoardgameDoesNotExist()
        {
            // Arrange
            var request = new UpdateWishlistRequest
            {
                WishlistId = 1,
                WishlistName = "Updated Wishlist",
                WishlistAccountId = 1,
                WishlistBoardgameIds = new List<int> { 1, 2, 3 }
            };

            var wishlist = new Wishlist { Id = 1, AccountId = 1, Name = "My Wishlist", Boardgames = new List<Boardgame>() };
            _unitOfWorkMock.WishlistRepository.GetByAccount(request.WishlistAccountId, request.WishlistId).Returns(wishlist);
            _unitOfWorkMock.BoardgameRepository.GetById(Arg.Any<int>()).Returns((Boardgame?)null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldUpdateWishlist_WhenRequestIsValid()
        {
            // Arrange
            var request = new UpdateWishlistRequest
            {
                WishlistId = 1,
                WishlistName = "Updated Wishlist",
                WishlistAccountId = 1,
                WishlistBoardgameIds = new List<int> { 1, 2, 3 }
            };

            var boardgame = new Boardgame { Id = 1, Name = "Chess" };
            var wishlist = new Wishlist { Id = 1, AccountId = 1, Name = "My Wishlist", Boardgames = new List<Boardgame>() };

            _unitOfWorkMock.WishlistRepository.GetByAccount(request.WishlistAccountId, request.WishlistId).Returns(wishlist);
            _unitOfWorkMock.BoardgameRepository.GetById(Arg.Any<int>()).Returns(boardgame);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.WishlistName, result.Name);
            Assert.Equal(request.WishlistId, result.Id);
            Assert.Equal(request.WishlistAccountId, result.AccountId);

            await _unitOfWorkMock.WishlistRepository.Received(request.WishlistBoardgameIds.Count).CreateItem(Arg.Any<int>(), wishlist);
            await _unitOfWorkMock.Received(1).Save();
        }
    }
}