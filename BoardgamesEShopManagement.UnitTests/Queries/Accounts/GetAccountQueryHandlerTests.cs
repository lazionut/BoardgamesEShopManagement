using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Accounts
{
    public class GetAccountQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetAccountQueryHandler _handler;

        public GetAccountQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetAccountQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnAccount_WhenAccountExists()
        {
            // Arrange
            var accountId = 1;
            var expectedAccount = new Account { Id = accountId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            _unitOfWorkMock.AccountRepository.GetById(accountId).Returns(Task.FromResult<Account?>(expectedAccount));

            var query = new GetAccountQuery { AccountId = accountId };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedAccount.Id, actualResult.Id);
            Assert.Equal(expectedAccount.FirstName, actualResult.FirstName);
            Assert.Equal(expectedAccount.LastName, actualResult.LastName);
            Assert.Equal(expectedAccount.Email, actualResult.Email);

            await _unitOfWorkMock.AccountRepository.Received(1).GetById(accountId);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            var accountId = 1;

            _unitOfWorkMock.AccountRepository.GetById(accountId).Returns(Task.FromResult<Account?>(null));

            var query = new GetAccountQuery { AccountId = accountId };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.AccountRepository.Received(1).GetById(accountId);
        }
    }
}