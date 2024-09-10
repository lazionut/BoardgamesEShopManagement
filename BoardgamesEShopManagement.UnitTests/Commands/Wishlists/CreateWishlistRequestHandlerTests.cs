using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Wishlists
{
    public class CreateWishlistRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly CreateWishlistRequestHandler _handler;

        public CreateWishlistRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new CreateWishlistRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            var request = new CreateWishlistRequest
            {
                WishlistAccountId = 1,
                WishlistName = "My Wishlist",
                WishlistBoardgameIds = new List<int> { 1, 2, 3 }
            };

            _unitOfWorkMock.AccountRepository.GetById(request.WishlistAccountId).Returns((Account?)null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBoardgameDoesNotExist()
        {
            // Arrange
            var request = new CreateWishlistRequest
            {
                WishlistAccountId = 1,
                WishlistName = "My Wishlist",
                WishlistBoardgameIds = new List<int> { 1, 2, 3 }
            };

            var account = new Account { Id = 1, FirstName = "John", LastName = "Doe" };
            _unitOfWorkMock.AccountRepository.GetById(request.WishlistAccountId).Returns(account);
            _unitOfWorkMock.BoardgameRepository.GetById(Arg.Any<int>()).Returns((Boardgame?)null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldCreateWishlist_WhenRequestIsValid()
        {
            // Arrange
            var request = new CreateWishlistRequest
            {
                WishlistAccountId = 1,
                WishlistName = "My Wishlist",
                WishlistBoardgameIds = new List<int> { 1, 2, 3 }
            };

            var account = new Account { Id = 1, FirstName = "John", LastName = "Doe" };
            var boardgame = new Boardgame { Id = 1, Name = "Chess" };

            _unitOfWorkMock.AccountRepository.GetById(request.WishlistAccountId).Returns(account);
            _unitOfWorkMock.BoardgameRepository.GetById(Arg.Any<int>()).Returns(boardgame);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.WishlistName, result.Name);
            Assert.Equal(request.WishlistAccountId, result.AccountId);

            await _unitOfWorkMock.WishlistRepository.Received(1).Create(Arg.Any<Wishlist>());
            await _unitOfWorkMock.Received(2).Save();
            await _unitOfWorkMock.WishlistRepository.Received(request.WishlistBoardgameIds.Count).CreateItem(Arg.Any<int>(), Arg.Any<Wishlist>());
        }
    }
}