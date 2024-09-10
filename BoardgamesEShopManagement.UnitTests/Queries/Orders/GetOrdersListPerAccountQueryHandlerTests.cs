using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListPerAccount;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Orders
{
    public class GetOrdersListPerAccountQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetOrdersListPerAccountQueryHandler _handler;

        public GetOrdersListPerAccountQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetOrdersListPerAccountQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnOrdersList_WhenOrdersExist()
        {
            // Arrange
            var accountId = 1;
            var orders = new List<Order>
            {
                new Order { Id = 1, FullName = "John Doe", Address = "123 Main St" },
                new Order { Id = 2, FullName = "Jane Doe", Address = "456 Elm St" }
            };

            var query = new GetOrdersListPerAccountQuery
            {
                OrderAccountId = accountId,
                OrderPageIndex = 1,
                OrderPageSize = 10
            };

            _unitOfWorkMock.OrderRepository.GetPerAccount(query.OrderAccountId, query.OrderPageIndex, query.OrderPageSize)
                .Returns(Task.FromResult<List<Order>>(orders));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(orders.Count, actualResult.Count);
            Assert.Equal(orders[0].Id, actualResult[0].Id);
            Assert.Equal(orders[1].Id, actualResult[1].Id);

            await _unitOfWorkMock.OrderRepository.Received(1).GetPerAccount(query.OrderAccountId, query.OrderPageIndex, query.OrderPageSize);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoOrdersExist()
        {
            // Arrange
            var accountId = 1;
            var query = new GetOrdersListPerAccountQuery
            {
                OrderAccountId = accountId,
                OrderPageIndex = 1,
                OrderPageSize = 10
            };

            _unitOfWorkMock.OrderRepository.GetPerAccount(query.OrderAccountId, query.OrderPageIndex, query.OrderPageSize)
                .Returns(Task.FromResult<List<Order>>(new List<Order>()));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Empty(actualResult);

            await _unitOfWorkMock.OrderRepository.Received(1).GetPerAccount(query.OrderAccountId, query.OrderPageIndex, query.OrderPageSize);
        }
    }
}