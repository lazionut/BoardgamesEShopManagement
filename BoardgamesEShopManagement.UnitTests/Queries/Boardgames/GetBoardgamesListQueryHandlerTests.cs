using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Boardgames
{
    public class GetBoardgamesListQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly GetBoardgamesListQueryHandler _handler;

        public GetBoardgamesListQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new GetBoardgamesListQueryHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnBoardgamesList_WhenBoardgamesExist()
        {
            // Arrange
            var boardgames = new List<Boardgame>
            {
                new Boardgame { Id = 1, Name = "Chess" },
                new Boardgame { Id = 2, Name = "Monopoly" }
            };

            var query = new GetBoardgamesListQuery
            {
                BoardgamePageIndex = 1,
                BoardgamePageSize = 10,
                BoardgameSortOrder = BoardgamesSortOrdersMode.NameAscending
            };

            _unitOfWorkMock.BoardgameRepository.GetAllSorted(query.BoardgamePageIndex, query.BoardgamePageSize, query.BoardgameSortOrder)
                .Returns(Task.FromResult<List<Boardgame>?>(boardgames));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(boardgames.Count, actualResult.Count);
            Assert.Equal(boardgames[0].Id, actualResult[0].Id);
            Assert.Equal(boardgames[1].Id, actualResult[1].Id);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetAllSorted(query.BoardgamePageIndex, query.BoardgamePageSize, query.BoardgameSortOrder);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenNoBoardgamesExist()
        {
            // Arrange
            var query = new GetBoardgamesListQuery
            {
                BoardgamePageIndex = 1,
                BoardgamePageSize = 10,
                BoardgameSortOrder = BoardgamesSortOrdersMode.NameAscending
            };

            _unitOfWorkMock.BoardgameRepository.GetAllSorted(query.BoardgamePageIndex, query.BoardgamePageSize, query.BoardgameSortOrder)
                .Returns(Task.FromResult<List<Boardgame>?>(null));

            // Act
            var actualResult = await _handler.Handle(query, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.BoardgameRepository.Received(1).GetAllSorted(query.BoardgamePageIndex, query.BoardgamePageSize, query.BoardgameSortOrder);
        }
    }
}