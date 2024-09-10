using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Reviews
{
    public class DeleteReviewRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly DeleteReviewRequestHandler _handler;

        public DeleteReviewRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new DeleteReviewRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnDeletedReview_WhenReviewExists()
        {
            // Arrange
            var reviewId = 1;
            var expectedReview = new Review { Id = reviewId, Title = "Great Game", Score = 5, Content = "I loved this game!" };

            _unitOfWorkMock.ReviewRepository.Delete(reviewId).Returns(Task.FromResult<Review?>(expectedReview));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            var command = new DeleteReviewRequest { ReviewId = reviewId };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedReview.Id, actualResult.Id);
            Assert.Equal(expectedReview.Title, actualResult.Title);
            Assert.Equal(expectedReview.Score, actualResult.Score);
            Assert.Equal(expectedReview.Content, actualResult.Content);

            await _unitOfWorkMock.ReviewRepository.Received(1).Delete(reviewId);
            await _unitOfWorkMock.Received(1).Save();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenReviewDoesNotExist()
        {
            // Arrange
            var reviewId = 1;

            _unitOfWorkMock.ReviewRepository.Delete(reviewId).Returns(Task.FromResult<Review?>(null));

            var command = new DeleteReviewRequest { ReviewId = reviewId };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.ReviewRepository.Received(1).Delete(reviewId);
            await _unitOfWorkMock.DidNotReceive().Save();
        }
    }
}