using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByNameCounter;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Boardgames
{
    public class GetBoardgamesListByNameCounterQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetBoardgamesListByNameCounterQueryHandler _handler;

        public GetBoardgamesListByNameCounterQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetBoardgamesListByNameCounterQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnBoardgamesCount_WhenBoardgamesExist()
        {
            // Arrange
            var expectedCount = 5;
            var query = new GetBoardgamesListByNameCounterQuery { BoardgameNameCharacters = "Ch" };

            _unitOfWorkMock.BoardgameRepository.GetPerNameCounter(query.BoardgameNameCharacters).Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetPerNameCounter(query.BoardgameNameCharacters);
        }

        [Fact]
        public async Task Handle_ShouldReturnZero_WhenNoBoardgamesExist()
        {
            // Arrange
            var expectedCount = 0;
            var query = new GetBoardgamesListByNameCounterQuery { BoardgameNameCharacters = "Ch" };

            _unitOfWorkMock.BoardgameRepository.GetPerNameCounter(query.BoardgameNameCharacters).Returns(Task.FromResult(expectedCount));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedCount, actualResult);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetPerNameCounter(query.BoardgameNameCharacters);
        }
    }
}