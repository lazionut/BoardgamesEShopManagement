using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReview;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Queries.Reviews
{
    public class GetReviewQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetReviewQueryHandler _handler;

        public GetReviewQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetReviewQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnReview_WhenReviewExists()
        {
            // Arrange
            var reviewId = 1;
            var expectedReview = new Review { Id = reviewId, Title = "Great Game", Content = "I loved this game!", Score = 5 };

            _unitOfWorkMock.ReviewRepository.GetById(reviewId).Returns(Task.FromResult<Review?>(expectedReview));

            var query = new GetReviewQuery { ReviewId = reviewId };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedReview.Id, actualResult.Id);
            Assert.Equal(expectedReview.Title, actualResult.Title);
            Assert.Equal(expectedReview.Content, actualResult.Content);
            Assert.Equal(expectedReview.Score, actualResult.Score);

            await _unitOfWorkMock.ReviewRepository.Received(1).GetById(reviewId);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenReviewDoesNotExist()
        {
            // Arrange
            var reviewId = 1;

            _unitOfWorkMock.ReviewRepository.GetById(reviewId).Returns(Task.FromResult<Review?>(null));

            var query = new GetReviewQuery { ReviewId = reviewId };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.ReviewRepository.Received(1).GetById(reviewId);
        }
    }
}