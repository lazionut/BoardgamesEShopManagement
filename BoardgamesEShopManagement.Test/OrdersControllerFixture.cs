using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MediatR;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder;
using BoardgamesEShopManagement.Application.Categories.Queries.GetOrder;

namespace BoardgamesEShopManagement.Test
{
    public class OrdersControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        /*
        [Fact]
        public async void Create_Order_CreateOrderCommandIsCalled()
        {
            CreateOrderRequest createOrderCommand = new CreateOrderRequest
            {
                OrderAccountId = 1,
                OrderBoardgameIds = new List<int> { 1, 9, 10 },
                OrderBoardgameQuantities = new List<int> { 1, 2, 3 }
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<CreateOrderRequest>(s => s == createOrderCommand), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                      new Order
                      {
                          Id = 11,
                          Total = 4778.76M,
                          AccountId = 1
                      }
                );

            OrdersController controller = new OrdersController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.CreateOrder(new OrderPostDto
            {
                OrderAccountId = 1,
                OrderBoardgameIds = new List<int> { 1, 9, 10 },
                OrderBoardgameQuantities = new List<int> { 1, 2, 3 }
            });

            CreatedAtActionResult okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(createOrderCommand.OrderAccountId, ((OrderGetDto)okResult.Value).OrderAccountId);
        }
        */

        [Fact]
        public async void Get_Order_GetOrderQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetOrderQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                new Order
                {
                    Id = 1,
                    Total = 4332.55M,
                    AccountId = 7
                });

            OrdersController controller = new OrdersController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.GetOrder(1);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }
    }
}
