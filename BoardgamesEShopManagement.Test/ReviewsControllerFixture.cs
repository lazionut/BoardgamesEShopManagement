using Xunit;
using MediatR;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReview;
using BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.API.Services;

namespace BoardgamesEShopManagement.Test
{
    public class ReviewsControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ISingletonService> _mockSingleton = new Mock<ISingletonService>();

        [Fact]
        public async void Create_Review_CreateReviewCommandIsCalled()
        {
            CreateReviewRequest createReviewCommand = new CreateReviewRequest
            {
                ReviewTitle = "ReviewTitle",
                ReviewAuthor = "ReviewAuthor",
                ReviewScore = 3,
                ReviewContent = "ReviewContent",
                ReviewBoardgameId = 3,
                ReviewAccountId = 7
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<CreateReviewRequest>(s => s == createReviewCommand), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                      new Review
                      {
                          Title = "ReviewTitle",
                          Author = "ReviewAuthor",
                          Score = 3,
                          Content = "ReviewContent",
                          BoardgameId = 3,
                          AccountId = 7
                      }
                );

            _mockMapper
               .Setup(m => m.Map<ReviewGetDto>(It.IsAny<Review>()))
               .Returns(new ReviewGetDto
               {
                   Title = "ReviewTitle",
                   Author = "ReviewAuthor",
                   Score = 3,
                   Content = "ReviewContent",
                   BoardgameId = 3,
                   AccountId = 7
               }
               );

            ReviewsController controller = new ReviewsController(_mockMediator.Object, _mockMapper.Object, _mockSingleton.Object);

            IActionResult result = await controller.CreateReview(new ReviewPostDto
            {
                Title = "ReviewTitle",
                Author = "ReviewAuthor",
                Score = 3,
                Content = "ReviewContent",
                BoardgameId = 3,
            });

            CreatedAtActionResult okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(createReviewCommand.ReviewTitle, ((ReviewGetDto)okResult.Value).Title);
        }

        [Fact]
        public async void Get_Review_GetReviewQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetReviewQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                new Review
                {
                    Id = 1,
                    Title = "Review1",
                    Score = 3,
                    Content = "The box this comes in is 5 foot by 6 inch and weights 17 pound!!!",
                    BoardgameId = 3,
                    AccountId = 7
                });

            ReviewsController controller = new ReviewsController(_mockMediator.Object, _mockMapper.Object, _mockSingleton.Object);

            IActionResult result = await controller.GetReview(1);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async void Delete_Review_DeleteReviewCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.Is<GetReviewQuery>(s => s.ReviewId == 1), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Review() { });

            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteReviewRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Review
                {
                    Id = 1,
                    Title = "Review1",
                    Score = 3,
                    Content = "The box this comes in is 5 foot by 6 inch and weights 17 pound!!!",
                    BoardgameId = 3,
                    AccountId = 7
                });

            ReviewsController controller = new ReviewsController(_mockMediator.Object, _mockMapper.Object, _mockSingleton.Object);

            await controller.DeleteReview(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteReviewRequest>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
