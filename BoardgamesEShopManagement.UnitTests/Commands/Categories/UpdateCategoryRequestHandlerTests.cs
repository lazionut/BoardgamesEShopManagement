using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Categories.Commands.UpdateCategory;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Categories
{
    public class UpdateCategoryRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly UpdateCategoryRequestHandler _handler;

        public UpdateCategoryRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new UpdateCategoryRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            var request = new UpdateCategoryRequest
            {
                CategoryId = 1,
                CategoryName = "Updated Category"
            };

            _unitOfWorkMock.CategoryRepository.GetById(request.CategoryId).Returns(Task.FromResult<Category?>(null));

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.CategoryRepository.Received(1).GetById(request.CategoryId);
            _unitOfWorkMock.CategoryRepository.DidNotReceive().Update(Arg.Any<Category>());
            await _unitOfWorkMock.DidNotReceive().Save();
        }

        [Fact]
        public async Task Handle_ShouldUpdateCategory_WhenRequestIsValid()
        {
            // Arrange
            var existingCategory = new Category
            {
                Id = 1,
                Name = "Existing Category"
            };

            var request = new UpdateCategoryRequest
            {
                CategoryId = 1,
                CategoryName = "Updated Category"
            };

            _unitOfWorkMock.CategoryRepository.GetById(request.CategoryId).Returns(Task.FromResult<Category?>(existingCategory));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.CategoryName, result.Name);
            Assert.Equal(existingCategory.UpdatedAt, result.UpdatedAt);

            await _unitOfWorkMock.CategoryRepository.Received(1).GetById(request.CategoryId);
            _unitOfWorkMock.CategoryRepository.Received(1).Update(existingCategory);
            await _unitOfWorkMock.Received(1).Save();
        }
    }
}