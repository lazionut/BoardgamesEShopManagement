using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MediatR;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview;
using BoardgamesEShopManagement.Application.Categories.Queries.GetReview;
using BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview;
using BoardgamesEShopManagement.Application.Reviews.Commands.UpdateReview;

namespace BoardgamesEShopManagement.Test
{
    public class ReviewsControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        /*
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

            ReviewsController controller = new ReviewsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.CreateReview(new ReviewPostDto
            {
                ReviewTitle = "ReviewTitle",
                ReviewAuthor = "ReviewAuthor",
                ReviewScore = 3,
                ReviewContent = "ReviewContent",
                ReviewBoardgameId = 3,
                ReviewAccountId = 7
            });

            CreatedAtActionResult okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(createCategoryCommand.ReviewTitle, ((ReviewGetDto)okResult.Value).ReviewTitle);
        }
        */

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

            ReviewsController controller = new ReviewsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.GetReview(1);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        /*
        [Fact]
        public async void Update_Review_UpdateReviewCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdateReviewRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Review
                {
                    Title = "ReviewTitle",
                    Content = "ReviewContent",
                });

            ReviewsController controller = new ReviewsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.UpdateReview(1, new ReviewPatchDto {
                ReviewTitle = "ReviewTitle",
                ReviewContent = "ReviewContent"
            });

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }
        */

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

            ReviewsController controller = new ReviewsController(_mockMediator.Object, _mockMapper.Object);

            await controller.DeleteReview(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteReviewRequest>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
