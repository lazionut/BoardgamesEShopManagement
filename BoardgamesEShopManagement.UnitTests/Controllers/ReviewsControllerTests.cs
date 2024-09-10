using AutoMapper;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview;
using BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReview;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Controllers
{
    public class ReviewsControllerTests : CustomControllerBaseMock
    {
        private readonly IMediator _mediatorMock;
        private readonly IMapper _mapperMock;
        private readonly ReviewsController _controller;

        public ReviewsControllerTests()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _mapperMock = Substitute.For<IMapper>();
            _controller = new ReviewsController(_mediatorMock, _mapperMock)
            {
                ControllerContext = ControllerContext
            };
        }

        [Fact]
        public async Task CreateReview_ValidReview_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var reviewPostDto = new ReviewPostDto
            {
                Title = "Great Game",
                Score = 5,
                Content = "I loved this game!",
                BoardgameId = 1
            };

            var review = new Review
            {
                Id = 1,
                Title = "Great Game",
                Score = 5,
                Content = "I loved this game!",
                BoardgameId = 1,
                AccountId = 1
            };

            _mediatorMock.Send(Arg.Any<CreateReviewRequest>()).Returns(review);
            _mapperMock.Map<ReviewGetDto>(Arg.Any<Review>()).Returns(new ReviewGetDto { Id = 1 });

            // Act
            var result = await _controller.CreateReview(reviewPostDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetReview), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task GetReview_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            var review = new Review
            {
                Id = 1,
                Title = "Great Game",
                Score = 5,
                Content = "I loved this game!",
                BoardgameId = 1,
                AccountId = 1
            };

            _mediatorMock.Send(Arg.Any<GetReviewQuery>()).Returns(review);
            _mapperMock.Map<ReviewGetDto>(Arg.Any<Review>()).Returns(new ReviewGetDto { Id = 1 });

            // Act
            var result = await _controller.GetReview(1);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ReviewGetDto>(okObjectResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task DeleteReview_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var review = new Review
            {
                Id = 1,
                Title = "Great Game",
                Score = 5,
                Content = "I loved this game!",
                BoardgameId = 1,
                AccountId = 1
            };

            _mediatorMock.Send(Arg.Any<DeleteReviewRequest>()).Returns(review);

            // Act
            var result = await _controller.DeleteReview(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}