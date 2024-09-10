using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Boardgames
{
    public class CreateBoardgameRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly CreateBoardgameRequestHandler _handler;

        public CreateBoardgameRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new CreateBoardgameRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            var command = new CreateBoardgameRequest
            {
                BoardgameName = "Test Boardgame",
                BoardgameCategoryId = 1
            };

            _unitOfWorkMock.CategoryRepository.GetById(command.BoardgameCategoryId).Returns(Task.FromResult<Category?>(null));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.CategoryRepository.Received(1).GetById(command.BoardgameCategoryId);
            await _unitOfWorkMock.BoardgameRepository.DidNotReceive().Create(Arg.Any<Boardgame>());
            await _unitOfWorkMock.DidNotReceive().Save();
        }

        [Fact]
        public async Task Handle_ShouldCreateBoardgame_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Strategy" };
            var command = new CreateBoardgameRequest
            {
                BoardgameImage = "image.png",
                BoardgameName = "Test Boardgame",
                BoardgameReleaseYear = 2022,
                BoardgameDescription = "A test boardgame",
                BoardgamePrice = 29.99m,
                BoardgameLink = "http://example.com",
                BoardgameQuantity = 10,
                BoardgameCategoryId = 1
            };

            _unitOfWorkMock.CategoryRepository.GetById(command.BoardgameCategoryId).Returns(Task.FromResult<Category?>(category));
            _unitOfWorkMock.BoardgameRepository.Create(Arg.Any<Boardgame>()).Returns(Task.CompletedTask);
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.BoardgameName, result.Name);
            Assert.Equal(command.BoardgameCategoryId, result.CategoryId);
            Assert.False(result.IsArchived);

            await _unitOfWorkMock.CategoryRepository.Received(1).GetById(command.BoardgameCategoryId);
            await _unitOfWorkMock.BoardgameRepository.Received(1).Create(Arg.Any<Boardgame>());
            await _unitOfWorkMock.Received(1).Save();
        }
    }
}