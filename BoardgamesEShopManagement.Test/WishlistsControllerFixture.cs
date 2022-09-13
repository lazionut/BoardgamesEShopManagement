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
using BoardgamesEShopManagement.Application.Categories.Queries.GetWishlist;

namespace BoardgamesEShopManagement.Test
{
    public class WishlistsControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        [Fact]
        public async void Create_Wishlist_CreateWishlistCommandIsCalled()
        {
            CreateWishlistRequest createWishlistRequest = new CreateWishlistRequest
            {
                WishlistName = "My wishlist",
                WishlistAccountId = 1,
                WishlistBoardgameIds = new List<int> { }
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<CreateWishlistRequest>(s => s == createWishlistRequest), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                      new Wishlist
                      {
                          Name = "My wishlist",
                          AccountId = 1,
                      }
                );

            _mockMediator
                 .Setup(m => m.Send(It.IsAny<WishlistGetDto>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new Wishlist
                 {
                     Name = "My wishlist",
                     AccountId = 1,
                 });

            WishlistsController controller = new WishlistsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.CreateWishlist(new WishlistPostDto
            {
                WishlistName = "My wishlist",
                WishlistAccountId = 1,
                WishlistBoardgameIds = new List<int> { }
            });

            CreatedAtActionResult okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(createWishlistRequest.WishlistAccountId, ((WishlistGetDto)okResult.Value).WishlistAccountId);
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

            WishlistsController controller = new WishlistsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.GetWishlist(1);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }
    }
}
