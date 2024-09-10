using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Categories.Commands.CreateCategory;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Categories
{
    public class CreateCategoryRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly CreateCategoryRequestHandler _handler;

        public CreateCategoryRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new CreateCategoryRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldCreateCategory_WhenRequestIsValid()
        {
            // Arrange
            var command = new CreateCategoryRequest
            {
                CategoryName = "New Category"
            };

            _unitOfWorkMock.CategoryRepository.Create(Arg.Any<Category>()).Returns(Task.CompletedTask);
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.CategoryName, result.Name);

            await _unitOfWorkMock.CategoryRepository.Received(1).Create(Arg.Any<Category>());
            await _unitOfWorkMock.Received(1).Save();
        }
    }
}