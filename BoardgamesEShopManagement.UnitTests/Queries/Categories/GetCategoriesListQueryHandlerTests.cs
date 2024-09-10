using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Categories
{
    public class GetCategoriesListQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetCategoriesListQueryHandler _handler;

        public GetCategoriesListQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetCategoriesListQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnCategoriesList_WhenCategoriesExist()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Strategy" },
                new Category { Id = 2, Name = "Family" }
            };

            _unitOfWorkMock.CategoryRepository.GetCategoryCounter().Returns(Task.FromResult(2));
            _unitOfWorkMock.CategoryRepository.GetAll(1, 2).Returns(Task.FromResult<List<Category>?>(categories));

            var query = new GetCategoriesListQuery();

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(categories.Count, actualResult.Count);
            Assert.Equal(categories[0].Id, actualResult[0].Id);
            Assert.Equal(categories[1].Id, actualResult[1].Id);

            await _unitOfWorkMock.CategoryRepository.Received(1).GetCategoryCounter();
            await _unitOfWorkMock.CategoryRepository.Received(1).GetAll(1, 2);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenNoCategoriesExist()
        {
            // Arrange
            _unitOfWorkMock.CategoryRepository.GetCategoryCounter().Returns(Task.FromResult(0));
            _unitOfWorkMock.CategoryRepository.GetAll(1, 0).Returns(Task.FromResult<List<Category>?>(null));

            var query = new GetCategoriesListQuery();

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.CategoryRepository.Received(1).GetCategoryCounter();
            await _unitOfWorkMock.CategoryRepository.Received(1).GetAll(1, 0);
        }
    }
}