using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Boardgames
{
    public class GetBoardgamesListCounterQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetBoardgamesListCounterQueryHandler _handler;

        public GetBoardgamesListCounterQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetBoardgamesListCounterQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnBoardgamesCount_WhenBoardgamesExist()
        {
            // Arrange
            var expectedCount = 5;
            var query = new GetBoardgamesListCounterQuery();

            _unitOfWorkMock.BoardgameRepository.GetAllSortedCounter().Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetAllSortedCounter();
        }

        [Fact]
        public async Task Handle_ShouldReturnZero_WhenNoBoardgamesExist()
        {
            // Arrange
            var expectedCount = 0;
            var query = new GetBoardgamesListCounterQuery();

            _unitOfWorkMock.BoardgameRepository.GetAllSortedCounter().Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetAllSortedCounter();
        }
    }
}