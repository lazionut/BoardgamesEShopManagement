using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgame;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Queries.Reviews
{
    public class GetReviewsListPerBoardgameQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetReviewsListPerBoardgameQueryHandler _handler;

        public GetReviewsListPerBoardgameQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetReviewsListPerBoardgameQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnReviewsList_WhenReviewsExist()
        {
            // Arrange
            var boardgameId = 1;
            var reviews = new List<Review>
            {
                new Review { Id = 1, Title = "Great Game", Content = "I loved this game!", Score = 5 },
                new Review { Id = 2, Title = "Not bad", Content = "It was okay.", Score = 3 }
            };

            var query = new GetReviewsListPerBoardgameQuery
            {
                ReviewBoardgameId = boardgameId,
                ReviewPageIndex = 1,
                ReviewPageSize = 10
            };

            _unitOfWorkMock.ReviewRepository.GetPerBoardgame(query.ReviewBoardgameId, query.ReviewPageIndex, query.ReviewPageSize)
                .Returns(Task.FromResult<List<Review>?>(reviews));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(reviews.Count, actualResult.Count);
            Assert.Equal(reviews[0].Id, actualResult[0].Id);
            Assert.Equal(reviews[1].Id, actualResult[1].Id);

            await _unitOfWorkMock.ReviewRepository.Received(1).GetPerBoardgame(query.ReviewBoardgameId, query.ReviewPageIndex, query.ReviewPageSize);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoReviewsExist()
        {
            // Arrange
            var boardgameId = 1;
            var query = new GetReviewsListPerBoardgameQuery
            {
                ReviewBoardgameId = boardgameId,
                ReviewPageIndex = 1,
                ReviewPageSize = 10
            };

            _unitOfWorkMock.ReviewRepository.GetPerBoardgame(query.ReviewBoardgameId, query.ReviewPageIndex, query.ReviewPageSize)
                .Returns(Task.FromResult<List<Review>?>(new List<Review>()));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Empty(actualResult);

            await _unitOfWorkMock.ReviewRepository.Received(1).GetPerBoardgame(query.ReviewBoardgameId, query.ReviewPageIndex, query.ReviewPageSize);
        }
    }
}