using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Orders
{
    public class CreateOrderRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly CreateOrderRequestHandler _handler;

        public CreateOrderRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new CreateOrderRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            var request = new CreateOrderRequest
            {
                OrderFullName = "John Doe",
                OrderAddress = "123 Main St",
                OrderBoardgameIds = new List<int> { 1, 2 },
                OrderBoardgameQuantities = new List<int> { 1, 2 },
                OrderAccountId = 1
            };

            _unitOfWorkMock.AccountRepository.GetById(request.OrderAccountId).Returns(Task.FromResult<Account?>(null));

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.AccountRepository.Received(1).GetById(request.OrderAccountId);
            await _unitOfWorkMock.OrderRepository.DidNotReceive().Create(Arg.Any<Order>());
            await _unitOfWorkMock.DidNotReceive().Save();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBoardgameDoesNotExist()
        {
            // Arrange
            var account = new Account { Id = 1, FirstName = "John", LastName = "Doe" };
            var request = new CreateOrderRequest
            {
                OrderFullName = "John Doe",
                OrderAddress = "123 Main St",
                OrderBoardgameIds = new List<int> { 1, 2 },
                OrderBoardgameQuantities = new List<int> { 1, 2 },
                OrderAccountId = 1
            };

            _unitOfWorkMock.AccountRepository.GetById(request.OrderAccountId).Returns(Task.FromResult<Account?>(account));
            _unitOfWorkMock.BoardgameRepository.GetById(1).Returns(Task.FromResult<Boardgame?>(null));

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.AccountRepository.Received(1).GetById(request.OrderAccountId);
            await _unitOfWorkMock.BoardgameRepository.Received(1).GetById(1);
            await _unitOfWorkMock.OrderRepository.Received(1).Create(Arg.Any<Order>());
            await _unitOfWorkMock.Received(2).Save();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBoardgameQuantityIsInsufficient()
        {
            // Arrange
            var account = new Account { Id = 1, FirstName = "John", LastName = "Doe" };
            var boardgame = new Boardgame { Id = 1, Name = "Test Boardgame", Quantity = 1, Price = 50 };
            var request = new CreateOrderRequest
            {
                OrderFullName = "John Doe",
                OrderAddress = "123 Main St",
                OrderBoardgameIds = new List<int> { 1 },
                OrderBoardgameQuantities = new List<int> { 2 },
                OrderAccountId = 1
            };

            _unitOfWorkMock.AccountRepository.GetById(request.OrderAccountId).Returns(Task.FromResult<Account?>(account));
            _unitOfWorkMock.BoardgameRepository.GetById(1).Returns(Task.FromResult<Boardgame?>(boardgame));

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.AccountRepository.Received(1).GetById(request.OrderAccountId);
            await _unitOfWorkMock.BoardgameRepository.Received(2).GetById(1);
            await _unitOfWorkMock.OrderRepository.Received(1).Delete(Arg.Any<int>());
            await _unitOfWorkMock.Received(2).Save();
        }

        [Fact]
        public async Task Handle_ShouldCreateOrder_WhenRequestIsValid()
        {
            // Arrange
            var account = new Account { Id = 1, FirstName = "John", LastName = "Doe" };
            var boardgame1 = new Boardgame { Id = 1, Name = "Test Boardgame 1", Quantity = 10, Price = 50 };
            var boardgame2 = new Boardgame { Id = 2, Name = "Test Boardgame 2", Quantity = 10, Price = 30 };
            var request = new CreateOrderRequest
            {
                OrderFullName = "John Doe",
                OrderAddress = "123 Main St",
                OrderBoardgameIds = new List<int> { 1, 2 },
                OrderBoardgameQuantities = new List<int> { 1, 2 },
                OrderAccountId = 1
            };

            _unitOfWorkMock.AccountRepository.GetById(request.OrderAccountId).Returns(Task.FromResult<Account?>(account));
            _unitOfWorkMock.BoardgameRepository.GetById(1).Returns(Task.FromResult<Boardgame?>(boardgame1));
            _unitOfWorkMock.BoardgameRepository.GetById(2).Returns(Task.FromResult<Boardgame?>(boardgame2));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.OrderFullName, result.FullName);
            Assert.Equal(request.OrderAddress, result.Address);
            Assert.Equal(request.OrderAccountId, result.AccountId);
            Assert.Equal(110, result.Total); // 1*50 + 2*30 = 110
            Assert.Equal(2, result.OrderItems.Count);

            await _unitOfWorkMock.AccountRepository.Received(1).GetById(request.OrderAccountId);
            await _unitOfWorkMock.BoardgameRepository.Received(1).GetById(1);
            await _unitOfWorkMock.BoardgameRepository.Received(1).GetById(2);
            await _unitOfWorkMock.OrderRepository.Received(1).Create(Arg.Any<Order>());
            await _unitOfWorkMock.Received(2).Save();
        }
    }
}