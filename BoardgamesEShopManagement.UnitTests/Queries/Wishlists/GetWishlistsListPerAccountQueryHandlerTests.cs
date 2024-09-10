using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistsListPerAccount;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Queries.Wishlists
{
    public class GetWishlistsListPerAccountQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetWishlistsListPerAccountQueryHandler _handler;

        public GetWishlistsListPerAccountQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetWishlistsListPerAccountQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnWishlists_WhenWishlistsExistForAccount()
        {
            // Arrange
            var accountId = 1;
            var wishlists = new List<Wishlist>
            {
                new Wishlist { Id = 1, AccountId = accountId },
                new Wishlist { Id = 2, AccountId = accountId }
            };
            _unitOfWorkMock.WishlistRepository.GetPerAccount(accountId).Returns(Task.FromResult(wishlists));
            var query = new GetWishlistsListPerAccountQuery { WishlistAccountId = accountId };

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, wishlist => Assert.Equal(accountId, wishlist.AccountId));
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoWishlistsExistForAccount()
        {
            // Arrange
            var accountId = 1;
            _unitOfWorkMock.WishlistRepository.GetPerAccount(accountId).Returns(Task.FromResult(new List<Wishlist>()));
            var query = new GetWishlistsListPerAccountQuery { WishlistAccountId = accountId };

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}