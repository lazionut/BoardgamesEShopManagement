using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Categories.Commands.DeleteCategory;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Categories
{
    public class DeleteCategoryRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly DeleteCategoryRequestHandler _handler;

        public DeleteCategoryRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new DeleteCategoryRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnDeletedCategory_WhenCategoryExists()
        {
            // Arrange
            var categoryId = 1;
            var expectedCategory = new Category { Id = categoryId, Name = "Test Category" };

            _unitOfWorkMock.CategoryRepository.Delete(categoryId).Returns(Task.FromResult<Category?>(expectedCategory));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            var command = new DeleteCategoryRequest { CategoryId = categoryId };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedCategory.Id, actualResult.Id);
            Assert.Equal(expectedCategory.Name, actualResult.Name);

            await _unitOfWorkMock.CategoryRepository.Received(1).Delete(categoryId);
            await _unitOfWorkMock.Received(1).Save();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryId = 1;

            _unitOfWorkMock.CategoryRepository.Delete(categoryId).Returns(Task.FromResult<Category?>(null));

            var command = new DeleteCategoryRequest { CategoryId = categoryId };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.CategoryRepository.Received(1).Delete(categoryId);
            await _unitOfWorkMock.DidNotReceive().Save();
        }
    }
}