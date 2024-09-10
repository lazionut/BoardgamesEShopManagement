using AutoMapper;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Categories.Commands.CreateCategory;
using BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList;
using BoardgamesEShopManagement.Application.Categories.Queries.GetCategory;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Controllers
{
    public class CategoriesControllerTests
    {
        private readonly IMediator _mediatorMock;
        private readonly IMapper _mapperMock;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _mapperMock = Substitute.For<IMapper>();
            _controller = new CategoriesController(_mediatorMock, _mapperMock);
        }

        [Fact]
        public async Task CreateCategory_ValidCategory_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var categoryPostDto = new CategoryPostPutDto
            {
                Name = "New Category"
            };

            var category = new Category
            {
                Id = 1,
                Name = "New Category"
            };

            _mediatorMock.Send(Arg.Any<CreateCategoryRequest>()).Returns(category);
            _mapperMock.Map<CategoryGetDto>(Arg.Any<Category>()).Returns(new CategoryGetDto { Id = 1 });

            // Act
            var result = await _controller.CreateCategory(categoryPostDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetCategory), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task GetCategories_ReturnsOkObjectResult()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
            };

            _mediatorMock.Send(Arg.Any<GetCategoriesListQuery>()).Returns(categories);
            _mapperMock.Map<List<CategoryGetDto>>(Arg.Any<List<Category>>()).Returns(new List<CategoryGetDto>
            {
                new CategoryGetDto { Id = 1, Name = "Category 1" },
                new CategoryGetDto { Id = 2, Name = "Category 2" }
            });

            // Act
            var result = await _controller.GetCategories();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<CategoryGetDto>>(okObjectResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetCategory_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 1"
            };

            _mediatorMock.Send(Arg.Any<GetCategoryQuery>()).Returns(category);
            _mapperMock.Map<CategoryGetDto>(Arg.Any<Category>()).Returns(new CategoryGetDto { Id = 1, Name = "Category 1" });

            // Act
            var result = await _controller.GetCategory(1);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CategoryGetDto>(okObjectResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
    }
}