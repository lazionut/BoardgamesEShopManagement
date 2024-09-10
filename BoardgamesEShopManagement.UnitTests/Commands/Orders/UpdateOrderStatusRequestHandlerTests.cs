using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder;
using BoardgamesEShopManagement.Application.Orders.Commands.UpdateOrderStatus;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Orders
{
    public class UpdateOrderStatusRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly UpdateOrderStatusRequestHandler _handler;

        public UpdateOrderStatusRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new UpdateOrderStatusRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            // Arrange
            var request = new UpdateOrderStatusRequest
            {
                OrderId = 1,
                OrderStatus = OrderStatusMode.Processing
            };

            _unitOfWorkMock.OrderRepository.GetById(request.OrderId).Returns(Task.FromResult<Order?>(null));

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);
            await _unitOfWorkMock.OrderRepository.Received(1).GetById(request.OrderId);
            _unitOfWorkMock.OrderRepository.DidNotReceive().Update(Arg.Any<Order>());
            await _unitOfWorkMock.DidNotReceive().Save();
        }

        [Fact]
        public async Task Handle_ShouldUpdateOrderStatus_WhenOrderExists()
        {
            // Arrange
            var existingOrder = new Order
            {
                Id = 1,
                Status = OrderStatusMode.Processing
            };

            var request = new UpdateOrderStatusRequest
            {
                OrderId = 1,
                OrderStatus = OrderStatusMode.Processing
            };

            _unitOfWorkMock.OrderRepository.GetById(request.OrderId).Returns(Task.FromResult<Order?>(existingOrder));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.OrderStatus, result.Status);
            Assert.Equal(existingOrder.UpdatedAt, result.UpdatedAt);

            await _unitOfWorkMock.OrderRepository.Received(1).GetById(request.OrderId);
            _unitOfWorkMock.OrderRepository.Received(1).Update(existingOrder);
            await _unitOfWorkMock.Received(1).Save();
        }
    }
}