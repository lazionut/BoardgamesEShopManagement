using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListCounter;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Orders
{
    public class GetOrdersListCounterQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetOrdersListCounterQueryHandler _handler;

        public GetOrdersListCounterQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetOrdersListCounterQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnOrdersCount_WhenOrdersExist()
        {
            // Arrange
            var expectedCount = 5;
            var query = new GetOrdersListCounterQuery();

            _unitOfWorkMock.OrderRepository.GetAllCounter().Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.OrderRepository.Received(1).GetAllCounter();
        }

        [Fact]
        public async Task Handle_ShouldReturnZero_WhenNoOrdersExist()
        {
            // Arrange
            var expectedCount = 0;
            var query = new GetOrdersListCounterQuery();

            _unitOfWorkMock.OrderRepository.GetAllCounter().Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.OrderRepository.Received(1).GetAllCounter();
        }
    }
}