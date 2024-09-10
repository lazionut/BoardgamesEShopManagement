using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListNames;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Boardgames
{
    public class GetBoardgamesListNamesQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetBoardgamesListNamesQueryHandler _handler;

        public GetBoardgamesListNamesQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetBoardgamesListNamesQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnBoardgameNamesList_WhenBoardgamesExist()
        {
            // Arrange
            var boardgameNames = new List<string> { "Chess", "Monopoly" };

            _unitOfWorkMock.BoardgameRepository.GetNames().Returns(Task.FromResult<List<string>>(boardgameNames));

            var query = new GetBoardgamesListNamesQuery();

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(boardgameNames.Count, actualResult.Count);
            Assert.Equal(boardgameNames[0], actualResult[0]);
            Assert.Equal(boardgameNames[1], actualResult[1]);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetNames();
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoBoardgamesExist()
        {
            // Arrange
            _unitOfWorkMock.BoardgameRepository.GetNames().Returns(Task.FromResult<List<string>>(new List<string>()));

            var query = new GetBoardgamesListNamesQuery();

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Empty(actualResult);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetNames();
        }
    }
}