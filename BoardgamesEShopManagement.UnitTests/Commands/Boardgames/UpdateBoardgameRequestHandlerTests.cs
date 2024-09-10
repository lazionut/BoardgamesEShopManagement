using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Boardgames.Commands.UpdateBoardgame;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Boardgames
{
    public class UpdateBoardgameRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly UpdateBoardgameRequestHandler _handler;

        public UpdateBoardgameRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new UpdateBoardgameRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBoardgameDoesNotExist()
        {
            // Arrange
            var command = new UpdateBoardgameRequest
            {
                BoardgameId = 1,
                BoardgameName = "Updated Boardgame",
                BoardgameCategoryId = 1
            };

            _unitOfWorkMock.BoardgameRepository.GetById(command.BoardgameId).Returns(Task.FromResult<Boardgame?>(null));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetById(command.BoardgameId);
            await _unitOfWorkMock.CategoryRepository.DidNotReceive().GetById(command.BoardgameCategoryId);
            _unitOfWorkMock.BoardgameRepository.DidNotReceive().Update(Arg.Any<Boardgame>());
            await _unitOfWorkMock.DidNotReceive().Save();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            var existingBoardgame = new Boardgame
            {
                Id = 1,
                Name = "Existing Boardgame",
                CategoryId = 1
            };

            var command = new UpdateBoardgameRequest
            {
                BoardgameId = 1,
                BoardgameName = "Updated Boardgame",
                BoardgameCategoryId = 2
            };

            _unitOfWorkMock.BoardgameRepository.GetById(command.BoardgameId).Returns(Task.FromResult<Boardgame?>(existingBoardgame));
            _unitOfWorkMock.CategoryRepository.GetById(command.BoardgameCategoryId).Returns(Task.FromResult<Category?>(null));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetById(command.BoardgameId);
            await _unitOfWorkMock.CategoryRepository.Received(1).GetById(command.BoardgameCategoryId);
            _unitOfWorkMock.BoardgameRepository.DidNotReceive().Update(Arg.Any<Boardgame>());
            await _unitOfWorkMock.DidNotReceive().Save();
        }

        [Fact]
        public async Task Handle_ShouldUpdateBoardgame_WhenRequestIsValid()
        {
            // Arrange
            var existingBoardgame = new Boardgame
            {
                Id = 1,
                Name = "Existing Boardgame",
                CategoryId = 1
            };

            var category = new Category { Id = 2, Name = "Strategy" };

            var command = new UpdateBoardgameRequest
            {
                BoardgameId = 1,
                BoardgameImage = "updated_image.png",
                BoardgameName = "Updated Boardgame",
                BoardgameReleaseYear = 2023,
                BoardgameDescription = "Updated description",
                BoardgamePrice = 39.99m,
                BoardgameLink = "http://updatedlink.com",
                BoardgameQuantity = 20,
                BoardgameCategoryId = 2
            };

            _unitOfWorkMock.BoardgameRepository.GetById(command.BoardgameId).Returns(Task.FromResult<Boardgame?>(existingBoardgame));
            _unitOfWorkMock.CategoryRepository.GetById(command.BoardgameCategoryId).Returns(Task.FromResult<Category?>(category));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.BoardgameName, result.Name);
            Assert.Equal(command.BoardgameCategoryId, result.CategoryId);
            Assert.Equal(command.BoardgameImage, result.Image);
            Assert.Equal(command.BoardgameReleaseYear, result.ReleaseYear);
            Assert.Equal(command.BoardgameDescription, result.Description);
            Assert.Equal(command.BoardgamePrice, result.Price);
            Assert.Equal(command.BoardgameLink, result.Link);
            Assert.Equal(command.BoardgameQuantity, result.Quantity);
            Assert.Equal(existingBoardgame.UpdatedAt, result.UpdatedAt);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetById(command.BoardgameId);
            await _unitOfWorkMock.CategoryRepository.Received(1).GetById(command.BoardgameCategoryId);
            _unitOfWorkMock.BoardgameRepository.Received(1).Update(existingBoardgame);
            await _unitOfWorkMock.Received(1).Save();
        }
    }
}