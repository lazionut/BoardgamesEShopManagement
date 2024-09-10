using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategoryCounter;
using NSubstitute;
using Xunit;
using System.Threading;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Boardgames
{
    public class GetBoardgamesListPerCategoryCounterQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetBoardgamesListPerCategoryCounterQueryHandler _handler;

        public GetBoardgamesListPerCategoryCounterQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetBoardgamesListPerCategoryCounterQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnBoardgamesCount_WhenBoardgamesExist()
        {
            // Arrange
            var expectedCount = 5;
            var query = new GetBoardgamesListPerCategoryCounterQuery { CategoryId = 1 };

            _unitOfWorkMock.BoardgameRepository.GetPerCategoryCounter(query.CategoryId).Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetPerCategoryCounter(query.CategoryId);
        }

        [Fact]
        public async Task Handle_ShouldReturnZero_WhenNoBoardgamesExist()
        {
            // Arrange
            var expectedCount = 0;
            var query = new GetBoardgamesListPerCategoryCounterQuery { CategoryId = 1 };

            _unitOfWorkMock.BoardgameRepository.GetPerCategoryCounter(query.CategoryId).Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetPerCategoryCounter(query.CategoryId);
        }
    }
}