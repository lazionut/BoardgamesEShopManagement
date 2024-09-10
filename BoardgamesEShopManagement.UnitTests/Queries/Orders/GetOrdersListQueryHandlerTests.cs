using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersList;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Orders
{
    public class GetOrdersListQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetOrdersListQueryHandler _handler;

        public GetOrdersListQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetOrdersListQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnOrdersList_WhenOrdersExist()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, FullName = "John Doe", Address = "123 Main St" },
                new Order { Id = 2, FullName = "Jane Doe", Address = "456 Elm St" }
            };

            var query = new GetOrdersListQuery
            {
                OrderPageIndex = 1,
                OrderPageSize = 10
            };

            _unitOfWorkMock.OrderRepository.GetAll(query.OrderPageIndex, query.OrderPageSize).Returns(Task.FromResult<List<Order>>(orders));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(orders.Count, actualResult.Count);
            Assert.Equal(orders[0].Id, actualResult[0].Id);
            Assert.Equal(orders[1].Id, actualResult[1].Id);

            await _unitOfWorkMock.OrderRepository.Received(1).GetAll(query.OrderPageIndex, query.OrderPageSize);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoOrdersExist()
        {
            // Arrange
            var query = new GetOrdersListQuery
            {
                OrderPageIndex = 1,
                OrderPageSize = 10
            };

            _unitOfWorkMock.OrderRepository.GetAll(query.OrderPageIndex, query.OrderPageSize).Returns(Task.FromResult<List<Order>>(new List<Order>()));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Empty(actualResult);

            await _unitOfWorkMock.OrderRepository.Received(1).GetAll(query.OrderPageIndex, query.OrderPageSize);
        }
    }
}