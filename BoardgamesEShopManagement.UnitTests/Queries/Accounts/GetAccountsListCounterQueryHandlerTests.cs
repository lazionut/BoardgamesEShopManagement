using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsListCounter;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Accounts
{
    public class GetAccountsListCounterQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetAccountsListCounterQueryHandler _handler;

        public GetAccountsListCounterQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetAccountsListCounterQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnAccountsCount_WhenAccountsExist()
        {
            // Arrange
            var expectedCount = 5;
            var query = new GetAccountsListCounterQuery();

            _unitOfWorkMock.AccountRepository.GetAllCounter().Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.AccountRepository.Received(1).GetAllCounter();
        }

        [Fact]
        public async Task Handle_ShouldReturnZero_WhenNoAccountsExist()
        {
            // Arrange
            var expectedCount = 0;
            var query = new GetAccountsListCounterQuery();

            _unitOfWorkMock.AccountRepository.GetAllCounter().Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.AccountRepository.Received(1).GetAllCounter();
        }
    }
}