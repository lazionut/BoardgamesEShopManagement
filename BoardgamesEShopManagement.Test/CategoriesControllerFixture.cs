using Xunit;
using MediatR;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Categories.Commands.CreateCategory;
using BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList;
using BoardgamesEShopManagement.Application.Categories.Queries.GetCategory;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategory;
using BoardgamesEShopManagement.Application.Categories.Commands.UpdateCategory;
using BoardgamesEShopManagement.Application.Categories.Commands.DeleteCategory;

namespace BoardgamesEShopManagement.Test
{
    public class AddressesControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        [Fact]
        public async void Create_Category_CreateCategoryCommandIsCalled()
        {
            CreateCategoryRequest createCategoryCommand = new CreateCategoryRequest
            {
                CategoryName = "TestCategory"
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<CreateCategoryRequest>(s => s == createCategoryCommand), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                      new Category
                      {
                          Name = "TestCategory"
                      }
                );

            _mockMapper
                .Setup(m => m.Map<CategoryGetDto>(It.IsAny<Category>()))
                .Returns(new CategoryGetDto 
                { 
                    CategoryName = "TestCategory"
                });

            CategoriesController controller = new CategoriesController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.CreateCategory(new CategoryPostPutDto
            {
                CategoryName = "TestCategory"
            });

            CreatedAtActionResult okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(createCategoryCommand.CategoryName, ((CategoryGetDto)okResult.Value).CategoryName);
        }

        [Fact]
        public async void Get_Categories_List_GetCategoriesListQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetCategoriesListQuery>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            CategoriesController controller = new CategoriesController(_mockMediator.Object, _mockMapper.Object);

            await controller.GetCategories();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetCategoriesListQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void Get_Category_GetCategoryQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetCategoryQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                new Category
                {
                    Id = 1,
                    Name = "Casual"
                });

            CategoriesController controller = new CategoriesController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.GetCategory(1);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async void Get_Boardgames_List_Per_Category_GetBoardgamesListPerCategoryQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetBoardgamesListPerCategoryQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Boardgame> {
                    new Boardgame
                    {
                        Id = 3,
                        Image = null,
                        Name = "Warhammer 40K Collection",
                        Description = null,
                        Price = 538.99M,
                        Link = null,
                        Quantity = 43,
                        CategoryId = 4
                    }
                });

            CategoriesController controller = new CategoriesController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.GetBoardgamesPerCategory(4);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async void Update_Category_UpdateCategoryCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdateCategoryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category
                {
                    Id = 1,
                    Name = "UpdatedCategoryName"
                });

            _mockMapper
                .Setup(m => m.Map<CategoryPostPutDto>(It.IsAny<Category>()))
                .Returns(new CategoryPostPutDto
                { 
                    CategoryName = "UpdatedCategoryName"
                });

            CategoriesController controller = new CategoriesController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.UpdateCategory(1, new CategoryPostPutDto { CategoryName = "UpdatedCategoryName" });

            NoContentResult noContentResult = Assert.IsType<NoContentResult>(result);

            Assert.Equal((int)HttpStatusCode.NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async void Delete_Category_DeleteCategoryCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.Is<GetCategoryQuery>(s => s.CategoryId == 1), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category() { });

            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteCategoryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category
                {
                    Id = 1,
                    Name = "Casual"
                });

            CategoriesController controller = new CategoriesController(_mockMediator.Object, _mockMapper.Object);

            await controller.DeleteCategory(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteCategoryRequest>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
