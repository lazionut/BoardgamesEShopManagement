using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetByEmail;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Accounts
{
    public class GetAccountByEmailQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetAccountByEmailQueryHandler _handler;

        public GetAccountByEmailQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetAccountByEmailQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnAccount_WhenAccountExists()
        {
            // Arrange
            var accountEmail = "john.doe@example.com";
            var expectedAccount = new Account { Id = 1, FirstName = "John", LastName = "Doe", Email = accountEmail };

            _unitOfWorkMock.AccountRepository.GetByEmail(accountEmail).Returns(Task.FromResult<Account?>(expectedAccount));

            var query = new GetAccountByEmailQuery { AccountEmail = accountEmail };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedAccount.Id, actualResult.Id);
            Assert.Equal(expectedAccount.FirstName, actualResult.FirstName);
            Assert.Equal(expectedAccount.LastName, actualResult.LastName);
            Assert.Equal(expectedAccount.Email, actualResult.Email);

            await _unitOfWorkMock.AccountRepository.Received(1).GetByEmail(accountEmail);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            var accountEmail = "john.doe@example.com";

            _unitOfWorkMock.AccountRepository.GetByEmail(accountEmail).Returns(Task.FromResult<Account?>(null));

            var query = new GetAccountByEmailQuery { AccountEmail = accountEmail };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.AccountRepository.Received(1).GetByEmail(accountEmail);
        }
    }
}