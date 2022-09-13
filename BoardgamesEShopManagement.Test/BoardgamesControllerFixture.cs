using System.Net;
using Xunit;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListSorted;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByName;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.UpdateBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveBoardgame;

namespace BoardgamesEShopManagement.Test
{
    public class BoardgamesControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        [Fact]
        public async void Create_Boardgame_CreateBoardgameCommandIsCalled()
        {
            CreateBoardgameRequest createBoardgameCommand = new CreateBoardgameRequest
            {
                BoardgameImage = null,
                BoardgameName = "BoardgameName",
                BoardgameReleaseYear = 2010,
                BoardgameDescription = "BoardgameDescription",
                BoardgamePrice = 100M,
                BoardgameLink = null,
                BoardgameQuantity = 10,
                BoardgameCategoryId = 3
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<CreateBoardgameRequest>(s => s == createBoardgameCommand), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                      new Boardgame
                      {
                          Image = null,
                          Name = "BoardgameName",
                          ReleaseYear = 2010,
                          Description = "BoardgameDescription",
                          Price = 100M,
                          Link = null,
                          Quantity = 10,
                          CategoryId = 3
                      }
                );

            _mockMapper
                .Setup(m => m.Map<BoardgameGetDto>(It.IsAny<Boardgame>()))
                .Returns(
                new BoardgameGetDto
                     {
                        BoardgameImage = null,
                        BoardgameName = "BoardgameName",
                        BoardgameReleaseYear = 2010,
                        BoardgameDescription = "BoardgameDescription",
                        BoardgamePrice = 100M,
                        BoardgameLink = null,
                        BoardgameQuantity = 10,
                        BoardgameCategoryId = 3
                     }
                );

            BoardgamesController controller = new BoardgamesController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.CreateBoardgame(new BoardgamePostPutDto
            {
                BoardgameImage = null,
                BoardgameName = "BoardgameName",
                BoardgameReleaseYear = 2010,
                BoardgameDescription = "BoardgameDescription",
                BoardgamePrice = 100M,
                BoardgameLink = null,
                BoardgameQuantity = 10,
                BoardgameCategoryId = 3
            });

            CreatedAtActionResult okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(createBoardgameCommand.BoardgameName, ((BoardgameGetDto)okResult.Value).BoardgameName);
        }

        [Fact]
        public async void Get_Boardgames_List_GetBoardgamesListQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetBoardgamesListQuery>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            BoardgamesController controller = new BoardgamesController(_mockMediator.Object, _mockMapper.Object);

            await controller.GetBoardgames(1, 5);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetBoardgamesListQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void Get_Boardgames_List_Sorted_GetBoardgamesListSortedQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetBoardgamesListSortedQuery>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            BoardgamesController controller = new BoardgamesController(_mockMediator.Object, _mockMapper.Object);

            await controller.GetBoardgames(1, 5);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetBoardgamesListQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void Get_Boardgames_List_By_Name_GetBoardgamesListByNmaeQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetBoardgamesListByNameQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Boardgame> {
                new Boardgame
                {
                    Id = 5,
                    Image = null,
                    Name = "Patchwork",
                    Description = "PATCHWORK",
                    Price = 1106.00M,
                    Link = "https://boardgamegeek.com/boardgame/120677/terra-mystica",
                    Quantity = 23,
                    CategoryId = 9
                },
                new Boardgame
                {
                    Id = 6,
                    Image = null,
                    Name = "Patchwork",
                    Description = "PANDEMIC",
                    Price = 485.30M,
                    Link = "https://boardgamegeek.com/boardgame/171623/voyages-marco-polo",
                    Quantity = 88,
                    CategoryId = 3
                }
                });

            BoardgamesController controller = new BoardgamesController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.GetBoardgamesByName("a", 1, 5, BoardgamesSortOrdersEnum.ReleaseYearDescending);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async void Get_Boardgame_GetBoardgameQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetBoardgameQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                new Boardgame
                {
                    Id = 1,
                    Image = null,
                    Name = "Splendor",
                    Description = "SPLENDOR",
                    Price = 1456.48M,
                    Link = "https://boardgamegeek.com/boardgame/148228/splendor",
                    Quantity = 36,
                    CategoryId = 2
                });

            BoardgamesController controller = new BoardgamesController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.GetBoardgame(1);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async void Get_Reviews_List_Per_Boardgame_GetReviewsListPerBoardgameQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetReviewsListPerBoardgameQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Review> {
                new Review
                {
                    Id = 7,
                    Title = "Review7",
                    Author = "Katrina Hickle",
                    Score = 1,
                    Content = "My tiger loves to play with it.",
                    BoardgameId = 9,
                    AccountId = 6
                },
                new Review
                {
                    Id = 8,
                    Title = "Review8",
                    Author = "Lorene Botsford",
                    Score = 2,
                    Content = "I saw one of these in Tanzania and I bought one.",
                    BoardgameId = 9,
                    AccountId = 3
                },
                });

            BoardgamesController controller = new BoardgamesController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.GetReviewsPerBoardgame(9, 0, 5);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async void Update_Boardgame_UpdateBoardgameCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdateBoardgameRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Boardgame
                {
                    Image = "UpdatedImage",
                    Name = "UpdatedName",
                    Description = "UpdatedDescription",
                    Price = 300M,
                    Link = "BoardgameLink",
                    Quantity = 24,
                    CategoryId = 1
                });

            _mockMapper
                .Setup(m => m.Map<BoardgameGetDto>(It.IsAny<Boardgame>()))
                .Returns(new BoardgameGetDto
                {
                    BoardgameImage = null,
                    BoardgameName = "BoardgameName",
                    BoardgameDescription = "BoardgameDescription",
                    BoardgamePrice = 100M,
                    BoardgameLink = null,
                    BoardgameQuantity = 10,
                    BoardgameCategoryId = 3
                });

            BoardgamesController controller = new BoardgamesController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.UpdateBoardgame(1, new BoardgamePostPutDto
            {
                BoardgameImage = "UpdatedImage",
                BoardgameName = "UpdatedName",
                BoardgameDescription = "UpdatedDescription",
                BoardgamePrice = 300M,
                BoardgameLink = "BoardgameLink",
                BoardgameQuantity = 24,
                BoardgameCategoryId = 1
            });

            NoContentResult noContentResult = Assert.IsType<NoContentResult>(result);

            Assert.Equal((int)HttpStatusCode.NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async void Delete_Boardgame_DeleteBoardgameCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.Is<GetBoardgameQuery>(s => s.BoardgameId == 1), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Boardgame() { });

            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteBoardgameRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Boardgame
                {
                    Id = 1,
                    Image = null,
                    Name = "Splendor",
                    Description = "SPLENDOR",
                    Price = 1456.48M,
                    Link = "https://boardgamegeek.com/boardgame/148228/splendor",
                    Quantity = 36,
                    CategoryId = 2
                });

            BoardgamesController controller = new BoardgamesController(_mockMediator.Object, _mockMapper.Object);

            await controller.DeleteBoardgame(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteBoardgameRequest>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void Archive_Boardgame_ArchiveBoardgameCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.Is<ArchiveBoardgameRequest>(s => s.BoardgameId == 1), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Boardgame() { });

            _mockMediator
                .Setup(m => m.Send(It.IsAny<ArchiveBoardgameRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Boardgame
                {
                    Id = 1,
                    Image = null,
                    Name = "Splendor",
                    Description = "SPLENDOR",
                    Price = 1456.48M,
                    Link = "https://boardgamegeek.com/boardgame/148228/splendor",
                    Quantity = 36,
                    CategoryId = 2,
                    IsArchived = true
                });

            BoardgamesController controller = new BoardgamesController(_mockMediator.Object, _mockMapper.Object);

            await controller.ArchiveBoardgame(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<ArchiveBoardgameRequest>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
