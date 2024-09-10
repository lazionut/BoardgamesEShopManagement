using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrderByAccount;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Orders
{
    public class GetOrderByAccountQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetOrderByAccountQueryHandler _handler;

        public GetOrderByAccountQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetOrderByAccountQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var accountId = 1;
            var orderId = 1;
            var expectedOrder = new Order { Id = orderId, FullName = "John Doe", Address = "123 Main St" };

            _unitOfWorkMock.OrderRepository.GetByAccount(accountId, orderId).Returns(Task.FromResult<Order?>(expectedOrder));

            var query = new GetOrderByAccountQuery { OrderAccountId = accountId, OrderId = orderId };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedOrder.Id, actualResult.Id);
            Assert.Equal(expectedOrder.FullName, actualResult.FullName);
            Assert.Equal(expectedOrder.Address, actualResult.Address);

            await _unitOfWorkMock.OrderRepository.Received(1).GetByAccount(accountId, orderId);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            // Arrange
            var accountId = 1;
            var orderId = 1;

            _unitOfWorkMock.OrderRepository.GetByAccount(accountId, orderId).Returns(Task.FromResult<Order?>(null));

            var query = new GetOrderByAccountQuery { OrderAccountId = accountId, OrderId = orderId };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.OrderRepository.Received(1).GetByAccount(accountId, orderId);
        }
    }
}