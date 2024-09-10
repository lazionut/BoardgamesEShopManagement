using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Boardgames
{
    public class DeleteBoardgameRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly DeleteBoardgameRequestHandler _handler;

        public DeleteBoardgameRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new DeleteBoardgameRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnDeletedBoardgame_WhenBoardgameExists()
        {
            // Arrange
            var boardgameId = 1;
            var expectedBoardgame = new Boardgame { Id = boardgameId, Name = "Test Boardgame" };

            _unitOfWorkMock.BoardgameRepository.Delete(boardgameId).Returns(Task.FromResult<Boardgame?>(expectedBoardgame));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            var command = new DeleteBoardgameRequest { BoardgameId = boardgameId };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedBoardgame.Id, actualResult.Id);
            Assert.Equal(expectedBoardgame.Name, actualResult.Name);

            await _unitOfWorkMock.BoardgameRepository.Received(1).Delete(boardgameId);
            await _unitOfWorkMock.Received(1).Save();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBoardgameDoesNotExist()
        {
            // Arrange
            var boardgameId = 1;

            _unitOfWorkMock.BoardgameRepository.Delete(boardgameId).Returns(Task.FromResult<Boardgame?>(null));

            var command = new DeleteBoardgameRequest { BoardgameId = boardgameId };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.BoardgameRepository.Received(1).Delete(boardgameId);
            await _unitOfWorkMock.DidNotReceive().Save();
        }
    }
}