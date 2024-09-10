using AutoMapper;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.UpdateBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Controllers
{
    public class BoardgamesControllerTests : CustomControllerBaseMock
    {
        private readonly IMediator _mediatorMock;
        private readonly IMapper _mapperMock;
        private readonly BoardgamesController _controller;

        public BoardgamesControllerTests()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _mapperMock = Substitute.For<IMapper>();
            _controller = new BoardgamesController(_mediatorMock, _mapperMock)
            {
                ControllerContext = ControllerContext
            };
        }

        [Fact]
        public async Task CreateBoardgame_ValidBoardgame_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var boardgamePostDto = new BoardgamePostPutDto
            {
                Name = "New Boardgame",
                Description = "A fun new boardgame",
                Price = 29.99M
            };

            var boardgame = new Boardgame
            {
                Id = 1,
                Name = "New Boardgame",
                Description = "A fun new boardgame",
                Price = 29.99M
            };

            _mediatorMock.Send(Arg.Any<CreateBoardgameRequest>()).Returns(boardgame);
            _mapperMock.Map<BoardgameGetDto>(Arg.Any<Boardgame>()).Returns(new BoardgameGetDto { Id = 1 });

            // Act
            var result = await _controller.CreateBoardgame(boardgamePostDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetBoardgame), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task GetBoardgame_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            var boardgame = new Boardgame
            {
                Id = 1,
                Name = "Existing Boardgame",
                Description = "An existing boardgame",
                Price = 29.99M
            };

            _mediatorMock.Send(Arg.Any<GetBoardgameQuery>()).Returns(boardgame);
            _mapperMock.Map<BoardgameGetDto>(Arg.Any<Boardgame>()).Returns(new BoardgameGetDto { Id = 1 });

            // Act
            var result = await _controller.GetBoardgame(1);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BoardgameGetDto>(okObjectResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task UpdateBoardgame_ValidBoardgame_ReturnsNoContentResult()
        {
            // Arrange
            var boardgamePutDto = new BoardgamePostPutDto
            {
                Name = "Updated Boardgame",
                Description = "An updated boardgame",
                Price = 29.99M
            };

            var boardgame = new Boardgame
            {
                Id = 1,
                Name = "Updated Boardgame",
                Description = "An updated boardgame",
                Price = 29.99M
            };

            _mediatorMock.Send(Arg.Any<UpdateBoardgameRequest>()).Returns(boardgame);

            // Act
            var result = await _controller.UpdateBoardgame(1, boardgamePutDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBoardgame_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var boardgame = new Boardgame
            {
                Id = 1,
                Name = "Existing Boardgame",
                Description = "An existing boardgame",
                Price = 29.99M
            };

            _mediatorMock.Send(Arg.Any<DeleteBoardgameRequest>()).Returns(boardgame);

            // Act
            var result = await _controller.DeleteBoardgame(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}