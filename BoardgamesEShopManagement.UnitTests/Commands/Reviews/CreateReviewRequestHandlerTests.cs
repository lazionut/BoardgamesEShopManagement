using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Reviews
{
    public class CreateReviewRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly CreateReviewRequestHandler _handler;

        public CreateReviewRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new CreateReviewRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            var request = new CreateReviewRequest
            {
                ReviewTitle = "Great Game",
                ReviewScore = 5,
                ReviewContent = "I loved this game!",
                ReviewBoardgameId = 1,
                ReviewAccountId = 1
            };

            _unitOfWorkMock.AccountRepository.GetById(request.ReviewAccountId).Returns((Account?)null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBoardgameDoesNotExist()
        {
            // Arrange
            var request = new CreateReviewRequest
            {
                ReviewTitle = "Great Game",
                ReviewScore = 5,
                ReviewContent = "I loved this game!",
                ReviewBoardgameId = 1,
                ReviewAccountId = 1
            };

            var account = new Account { Id = 1, FirstName = "John", LastName = "Doe" };
            _unitOfWorkMock.AccountRepository.GetById(request.ReviewAccountId).Returns(account);
            _unitOfWorkMock.BoardgameRepository.GetById(request.ReviewBoardgameId).Returns((Boardgame?)null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBoardgameAlreadyReviewedByAccount()
        {
            // Arrange
            var request = new CreateReviewRequest
            {
                ReviewTitle = "Great Game",
                ReviewScore = 5,
                ReviewContent = "I loved this game!",
                ReviewBoardgameId = 1,
                ReviewAccountId = 1
            };

            var account = new Account { Id = 1, FirstName = "John", LastName = "Doe" };
            var boardgame = new Boardgame { Id = 1, Name = "Chess" };

            _unitOfWorkMock.AccountRepository.GetById(request.ReviewAccountId).Returns(account);
            _unitOfWorkMock.BoardgameRepository.GetById(request.ReviewBoardgameId).Returns(boardgame);
            _unitOfWorkMock.ReviewRepository.IsBoardgameReviewed(request.ReviewAccountId, request.ReviewBoardgameId).Returns(true);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldCreateReview_WhenRequestIsValid()
        {
            // Arrange
            var request = new CreateReviewRequest
            {
                ReviewTitle = "Great Game",
                ReviewScore = 5,
                ReviewContent = "I loved this game!",
                ReviewBoardgameId = 1,
                ReviewAccountId = 1
            };

            var account = new Account { Id = 1, FirstName = "John", LastName = "Doe" };
            var boardgame = new Boardgame { Id = 1, Name = "Chess" };

            _unitOfWorkMock.AccountRepository.GetById(request.ReviewAccountId).Returns(account);
            _unitOfWorkMock.BoardgameRepository.GetById(request.ReviewBoardgameId).Returns(boardgame);
            _unitOfWorkMock.ReviewRepository.IsBoardgameReviewed(request.ReviewAccountId, request.ReviewBoardgameId).Returns(false);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.ReviewTitle, result.Title);
            Assert.Equal(request.ReviewScore, result.Score);
            Assert.Equal(request.ReviewContent, result.Content);
            Assert.Equal(request.ReviewBoardgameId, result.BoardgameId);
            Assert.Equal(request.ReviewAccountId, result.AccountId);

            await _unitOfWorkMock.ReviewRepository.Received(1).Create(Arg.Any<Review>());
            await _unitOfWorkMock.Received(1).Save();
        }
    }
}