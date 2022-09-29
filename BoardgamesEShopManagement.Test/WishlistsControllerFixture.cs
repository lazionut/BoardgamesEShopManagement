using Xunit;
using MediatR;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlist;
using BoardgamesEShopManagement.API.Services;

namespace BoardgamesEShopManagement.Test
{
    public class WishlistsControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ISingletonService> _mockSingleton = new Mock<ISingletonService>();

        [Fact]
        public async void Create_Wishlist_CreateWishlistCommandIsCalled()
        {
            CreateWishlistRequest createWishlistCommand = new CreateWishlistRequest
            {
                WishlistName = "My wishlist",
                WishlistAccountId = 1,
                WishlistBoardgameIds = new List<int> { 1, 3 }
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<CreateWishlistRequest>(s => s == createWishlistCommand), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                      new Wishlist
                      {
                          Name = "My wishlist",
                          AccountId = 1,
                          Boardgames = new List<Boardgame>
                          {
                              new Boardgame 
                              {
                                  Image = null, 
                                  Name = "Splendor", 
                                  ReleaseYear = 2008, 
                                  Description = "SPLENDOR",
                                  Price = 150.92M,
                                  Link = "https://boardgamegeek.com/boardgame/230802/azul"
                              },
                              new Boardgame
                              {
                                  Image = null,
                                  Name = "Terra Mystica",
                                  ReleaseYear = 2019,
                                  Description = "TERRA MYSTICA",
                                  Price = 14672.72M,
                                  Link = "https://boardgamegeek.com/boardgame/148228/splendor"
                              }
                          }
                      }
                );

            _mockMapper
                 .Setup(m => m.Map<WishlistGetDto>(It.IsAny<Wishlist>()))
                 .Returns(new WishlistGetDto
                 {
                     Name = "My wishlist",
                     Boardgames = new List<WishlistBoardgameDto>
                          {
                              new WishlistBoardgameDto
                              {
                                  Image = null,
                                  Name = "Splendor",
                                  ReleaseYear = 2008,
                                  Description = "SPLENDOR",
                                  Price = 150.92M,
                                  Link = "https://boardgamegeek.com/boardgame/230802/azul"
                              },
                              new WishlistBoardgameDto
                              {
                                  Image = null,
                                  Name = "Terra Mystica",
                                  ReleaseYear = 2019,
                                  Description = "TERRA MYSTICA",
                                  Price = 14672.72M,
                                  Link = "https://boardgamegeek.com/boardgame/148228/splendor"
                              }
                          }
                 });

            WishlistsController controller = new WishlistsController(_mockMediator.Object, _mockMapper.Object, _mockSingleton.Object);

            IActionResult result = await controller.CreateWishlist(new WishlistPostDto
            {
                Name = "My wishlist",
                BoardgameIds = new List<int> { 1, 3 }
            });

            CreatedAtActionResult okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal((int)HttpStatusCode.Created, okResult.StatusCode);
        }

        [Fact]
        public async void Get_Wishlist_GetWishlistQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetWishlistQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                new Wishlist
                {
                    Name = "Wishlist1",
                    AccountId = 8
                });

            WishlistsController controller = new WishlistsController(_mockMediator.Object, _mockMapper.Object, _mockSingleton.Object);

            IActionResult result = await controller.GetWishlist(1);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }
    }
}
