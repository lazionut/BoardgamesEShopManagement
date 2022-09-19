using Xunit;
using MediatR;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrder;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist;

namespace BoardgamesEShopManagement.Test
{
    public class OrdersControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        [Fact]
        public async void Create_Order_CreateOrderCommandIsCalled()
        {
            CreateOrderRequest createOrderCommand = new CreateOrderRequest
            {
                OrderAccountId = 1,
                OrderBoardgameIds = new List<int> { 1, 9 },
                OrderBoardgameQuantities = new List<int> { 1, 1 }
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<CreateOrderRequest>(s => s == createOrderCommand), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                      new Order
                      {
                          Total = 1393.36M,
                          Status = 0,
                          AccountId = 1,
                          OrderItems = new List<OrderItem>
                          {
                              new OrderItem
                              {
                                  Boardgame = new Boardgame {                                  
                                      Image = null,
                                      Name = "Splendor",
                                      ReleaseYear = 2008,
                                      Description = "SPLENDOR",
                                      Price = 150.92M,
                                  Link = "https://boardgamegeek.com/boardgame/230802/azul" },
                                  Quantity = 1,
                              },
                              new OrderItem
                              {
                                 Boardgame = new Boardgame {
                                  Image = null,
                                  Name = "Warhammer 40K Collection",
                                  ReleaseYear = 2005,
                                  Description = "WARHAMMER 40K COLLECTION",
                                  Price = 1242.44M,
                                  Link = "https://boardgamegeek.com/boardgame/148228/splendor" },
                                  Quantity = 1,
                              },
                          }
                      }
            );

            _mockMapper
                 .Setup(m => m.Map<OrderGetDto>(It.IsAny<Order>()))
                 .Returns(new OrderGetDto
                 {
                     Total = 1393.36M,
                     Status = 0,
                     AccountId = 1,
                     Boardgames = new List<OrderBoardgameDto>
                          {
                              new OrderBoardgameDto
                              {
                                  Image = null,
                                  Name = "Splendor",
                                  Price = 150.92M
                              },
                              new OrderBoardgameDto
                              {
                                  Image = null,
                                  Name = "Warhammer 40K Collection",
                                  Price = 1242.44M
                              }
                          }
                 }
            );

            OrdersController controller = new OrdersController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.CreateOrder(new OrderPostDto
            {
                AccountId = 1,
                BoardgameIds = new List<int> { 1, 9 },
                BoardgameQuantities = new List<int> { 1, 1 }
            });

            CreatedAtActionResult okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(createOrderCommand.OrderAccountId, ((OrderGetDto)okResult.Value).AccountId);
        }

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
