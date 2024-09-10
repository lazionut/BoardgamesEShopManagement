using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveBoardgame;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Boardgames
{
    public class ArchiveBoardgameRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly ArchiveBoardgameRequestHandler _handler;

        public ArchiveBoardgameRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new ArchiveBoardgameRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBoardgameDoesNotExist()
        {
            // Arrange
            var command = new ArchiveBoardgameRequest { BoardgameId = 1 };
            _unitOfWorkMock.BoardgameRepository.GetById(command.BoardgameId).Returns(Task.FromResult<Boardgame?>(null));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetById(command.BoardgameId);
            await _unitOfWorkMock.DidNotReceive().Save();
        }

        [Fact]
        public async Task Handle_ShouldArchiveBoardgame_WhenBoardgameExists()
        {
            // Arrange
            var boardgame = new Boardgame { Id = 1, Name = "Test Boardgame", Price = 50, Quantity = 10, IsArchived = false };
            _unitOfWorkMock.BoardgameRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult<Boardgame?>(boardgame));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);
            var command = new ArchiveBoardgameRequest { BoardgameId = 1 };

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Price);
            Assert.Equal(0, result.Quantity);
            Assert.True(result.IsArchived);
            Assert.Equal(boardgame.UpdatedAt, result.UpdatedAt);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetById(command.BoardgameId);
            await _unitOfWorkMock.Received(1).Save();
        }
    }
}