using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Categories.Queries.GetCategory;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Categories
{
    public class GetCategoryQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetCategoryQueryHandler _handler;

        public GetCategoryQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetCategoryQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            var categoryId = 1;
            var expectedCategory = new Category { Id = categoryId, Name = "Strategy" };

            _unitOfWorkMock.CategoryRepository.GetById(categoryId).Returns(Task.FromResult<Category?>(expectedCategory));

            var query = new GetCategoryQuery { CategoryId = categoryId };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedCategory.Id, actualResult.Id);
            Assert.Equal(expectedCategory.Name, actualResult.Name);

            await _unitOfWorkMock.CategoryRepository.Received(1).GetById(categoryId);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryId = 1;

            _unitOfWorkMock.CategoryRepository.GetById(categoryId).Returns(Task.FromResult<Category?>(null));

            var query = new GetCategoryQuery { CategoryId = categoryId };

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.CategoryRepository.Received(1).GetById(categoryId);
        }
    }
}