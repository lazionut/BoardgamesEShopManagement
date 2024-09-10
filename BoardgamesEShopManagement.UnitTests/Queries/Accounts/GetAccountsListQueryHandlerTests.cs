using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsList;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Accounts
{
    public class GetAccountsListQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetAccountsListQueryHandler _handler;

        public GetAccountsListQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetAccountsListQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnAccountsList_WhenAccountsExist()
        {
            // Arrange
            var accounts = new List<Account>
            {
                new Account { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Account { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
            };

            var query = new GetAccountsListQuery { AccountPageIndex = 1, AccountPageSize = 10 };

            _unitOfWorkMock.AccountRepository.GetAll(query.AccountPageIndex, query.AccountPageSize).Returns(Task.FromResult<List<Account>?>(accounts));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(accounts.Count, actualResult.Count);
            Assert.Equal(accounts[0].Id, actualResult[0].Id);
            Assert.Equal(accounts[1].Id, actualResult[1].Id);

            await _unitOfWorkMock.AccountRepository.Received(1).GetAll(query.AccountPageIndex, query.AccountPageSize);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenNoAccountsExist()
        {
            // Arrange
            var query = new GetAccountsListQuery { AccountPageIndex = 1, AccountPageSize = 10 };

            _unitOfWorkMock.AccountRepository.GetAll(query.AccountPageIndex, query.AccountPageSize).Returns(Task.FromResult<List<Account>?>(null));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.AccountRepository.Received(1).GetAll(query.AccountPageIndex, query.AccountPageSize);
        }
    }
}