using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgameCounter;
using NSubstitute;
using Xunit;
using System.Threading;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.UnitTests.Queries.Reviews
{
    public class GetReviewsListPerBoardgameCounterQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetReviewsListPerBoardgameCounterQueryHandler _handler;

        public GetReviewsListPerBoardgameCounterQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetReviewsListPerBoardgameCounterQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnReviewsCount_WhenReviewsExist()
        {
            // Arrange
            var boardgameId = 1;
            var expectedCount = 5;
            var query = new GetReviewsListPerBoardgameCounterQuery { ReviewBoardgameId = boardgameId };

            _unitOfWorkMock.ReviewRepository.GetPerBoardgameCounter(boardgameId).Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.ReviewRepository.Received(1).GetPerBoardgameCounter(boardgameId);
        }

        [Fact]
        public async Task Handle_ShouldReturnZero_WhenNoReviewsExist()
        {
            // Arrange
            var boardgameId = 1;
            var expectedCount = 0;
            var query = new GetReviewsListPerBoardgameCounterQuery { ReviewBoardgameId = boardgameId };

            _unitOfWorkMock.ReviewRepository.GetPerBoardgameCounter(boardgameId).Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.ReviewRepository.Received(1).GetPerBoardgameCounter(boardgameId);
        }
    }
}