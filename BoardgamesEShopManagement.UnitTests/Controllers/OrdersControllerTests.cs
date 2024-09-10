using AutoMapper;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder;
using BoardgamesEShopManagement.Application.Orders.Commands.UpdateOrderStatus;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrder;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersList;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListCounter;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Controllers
{
    public class OrdersControllerTests : CustomControllerBaseMock
    {
        private readonly IMediator _mediatorMock;
        private readonly IMapper _mapperMock;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _mapperMock = Substitute.For<IMapper>();
            _controller = new OrdersController(_mediatorMock, _mapperMock)
            {
                ControllerContext = ControllerContext
            };
        }

        [Fact]
        public async Task CreateOrder_ValidOrder_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var orderPostDto = new OrderPostDto
            {
                BoardgameIds = new List<int> { 1, 2 },
                BoardgameQuantities = new List<int> { 1, 2 },
                FullName = "John Doe",
                Address = "123 Main St"
            };

            var order = new Order
            {
                Id = 1,
                FullName = "John Doe",
                Address = "123 Main St"
            };

            _mediatorMock.Send(Arg.Any<CreateOrderRequest>()).Returns(order);
            _mapperMock.Map<OrderGetDto>(Arg.Any<Order>()).Returns(new OrderGetDto { Id = 1 });

            // Act
            var result = await _controller.CreateOrder(orderPostDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetOrder), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task GetOrder_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                FullName = "John Doe",
                Address = "123 Main St"
            };

            _mediatorMock.Send(Arg.Any<GetOrderQuery>()).Returns(order);
            _mapperMock.Map<OrderGetDto>(Arg.Any<Order>()).Returns(new OrderGetDto { Id = 1 });

            // Act
            var result = await _controller.GetOrder(1);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<OrderGetDto>(okObjectResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task UpdateOrderStatus_ValidStatus_ReturnsNoContentResult()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                FullName = "John Doe",
                Address = "123 Main St",
                Status = OrderStatusMode.Processing
            };

            _mediatorMock.Send(Arg.Any<UpdateOrderStatusRequest>()).Returns(order);

            // Act
            var result = await _controller.UpdateOrderStatus(1, OrderStatusMode.Shipping);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}