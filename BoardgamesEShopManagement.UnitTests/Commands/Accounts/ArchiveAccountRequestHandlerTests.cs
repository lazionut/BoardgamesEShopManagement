using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Accounts.Commands.ArchiveAccount;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Accounts
{
    public class ArchiveAccountRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly ArchiveAccountRequestHandler _handler;

        public ArchiveAccountRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new ArchiveAccountRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            _unitOfWorkMock.AccountRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult<Account?>(null));

            var command = new ArchiveAccountRequest { AccountId = 1 };

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Null(result);
            await _unitOfWorkMock.AccountRepository.Received(1).GetById(1);
            await _unitOfWorkMock.DidNotReceive().Save();
        }

        [Fact]
        public async Task Handle_ShouldArchiveAccount_WhenAccountExists()
        {
            // Arrange
            var account = new Account { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", IsArchived = false };
            _unitOfWorkMock.AccountRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult<Account?>(account));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);
            var command = new ArchiveAccountRequest { AccountId = 1 };

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Anonymized", result.FirstName);
            Assert.Equal("Anonymized", result.LastName);
            Assert.Equal("Anonymized", result.Email);
            Assert.True(result.IsArchived);
            Assert.Equal(account.UpdatedAt, result.UpdatedAt);
            await _unitOfWorkMock.AccountRepository.Received(1).GetById(1);
            await _unitOfWorkMock.Received(1).Save();
        }
    }
}