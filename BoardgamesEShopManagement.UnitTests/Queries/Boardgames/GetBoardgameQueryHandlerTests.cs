using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Boardgames
{
    public class GetBoardgameQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetBoardgameQueryHandler _handler;

        public GetBoardgameQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetBoardgameQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnBoardgame_WhenBoardgameExists()
        {
            // Arrange
            var boardgameId = 1;
            var expectedBoardgame = new Boardgame { Id = boardgameId, Name = "Chess" };

            _unitOfWorkMock.BoardgameRepository.GetById(boardgameId).Returns(Task.FromResult<Boardgame?>(expectedBoardgame));

            var query = new GetBoardgameQuery { BoardgameId = boardgameId };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedBoardgame.Id, actualResult.Id);
            Assert.Equal(expectedBoardgame.Name, actualResult.Name);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetById(boardgameId);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBoardgameDoesNotExist()
        {
            // Arrange
            var boardgameId = 1;

            _unitOfWorkMock.BoardgameRepository.GetById(boardgameId).Returns(Task.FromResult<Boardgame?>(null));

            var query = new GetBoardgameQuery { BoardgameId = boardgameId };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetById(boardgameId);
        }
    }
}