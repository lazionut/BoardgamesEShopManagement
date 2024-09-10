using AutoMapper;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist;
using BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist;
using BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlistItem;
using BoardgamesEShopManagement.Application.Wishlists.Commands.UpdateWishlist;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlist;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Controllers
{
    public class WishlistsControllerTests : CustomControllerBaseMock
    {
        private readonly IMediator _mediatorMock;
        private readonly IMapper _mapperMock;
        private readonly WishlistsController _controller;

        public WishlistsControllerTests()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _mapperMock = Substitute.For<IMapper>();
            _controller = new WishlistsController(_mediatorMock, _mapperMock)
            {
                ControllerContext = ControllerContext
            };
        }

        [Fact]
        public async Task CreateWishlist_ValidWishlist_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var wishlistPostDto = new WishlistPostDto
            {
                Name = "My Wishlist",
                BoardgameIds = new List<int> { 1, 2 }
            };

            var wishlist = new Wishlist
            {
                Id = 1,
                Name = "My Wishlist",
                Boardgames = new List<Boardgame>
                {
                    new Boardgame { Id = 1 },
                    new Boardgame { Id = 2 }
                }
            };

            _mediatorMock.Send(Arg.Any<CreateWishlistRequest>()).Returns(wishlist);
            _mapperMock.Map<WishlistGetDto>(Arg.Any<Wishlist>()).Returns(new WishlistGetDto { Id = 1 });

            // Act
            var result = await _controller.CreateWishlist(wishlistPostDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetWishlist), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task GetWishlist_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            var wishlist = new Wishlist
            {
                Id = 1,
                Name = "My Wishlist",
                Boardgames = new List<Boardgame>
                {
                    new Boardgame { Id = 1 },
                    new Boardgame { Id = 2 }
                }
            };

            _mediatorMock.Send(Arg.Any<GetWishlistQuery>()).Returns(wishlist);
            _mapperMock.Map<WishlistGetDto>(Arg.Any<Wishlist>()).Returns(new WishlistGetDto { Id = 1 });

            // Act
            var result = await _controller.GetWishlist(1);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<WishlistGetDto>(okObjectResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
    }
}