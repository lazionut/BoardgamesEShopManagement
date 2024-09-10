using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListPerAccountCounter;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Orders
{
    public class GetOrdersListPerAccountCounterQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetOrdersListPerAccountCounterQueryHandler _handler;

        public GetOrdersListPerAccountCounterQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetOrdersListPerAccountCounterQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnOrdersCount_WhenOrdersExist()
        {
            // Arrange
            var accountId = 1;
            var expectedCount = 5;
            var query = new GetOrdersListPerAccountCounterQuery { OrderAccountId = accountId };

            _unitOfWorkMock.OrderRepository.GetPerAccountCounter(accountId).Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.OrderRepository.Received(1).GetPerAccountCounter(accountId);
        }

        [Fact]
        public async Task Handle_ShouldReturnZero_WhenNoOrdersExist()
        {
            // Arrange
            var accountId = 1;
            var expectedCount = 0;
            var query = new GetOrdersListPerAccountCounterQuery { OrderAccountId = accountId };

            _unitOfWorkMock.OrderRepository.GetPerAccountCounter(accountId).Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.OrderRepository.Received(1).GetPerAccountCounter(accountId);
        }
    }
}