using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Accounts.Commands.DeleteAccount;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Accounts
{
    public class DeleteAccountRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly DeleteAccountRequestHandler _handler;

        public DeleteAccountRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new DeleteAccountRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnDeletedAccount_WhenAccountExists()
        {
            // Arrange
            var accountId = 1;
            var expectedAccount = new Account { Id = accountId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            _unitOfWorkMock.AccountRepository.Delete(accountId).Returns(Task.FromResult<Account?>(expectedAccount));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            var command = new DeleteAccountRequest { AccountId = accountId };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedAccount.Id, actualResult.Id);
            Assert.Equal(expectedAccount.FirstName, actualResult.FirstName);
            Assert.Equal(expectedAccount.LastName, actualResult.LastName);
            Assert.Equal(expectedAccount.Email, actualResult.Email);

            await _unitOfWorkMock.AccountRepository.Received(1).Delete(accountId);
            await _unitOfWorkMock.Received(1).Save();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            var accountId = 1;

            _unitOfWorkMock.AccountRepository.Delete(accountId).Returns(Task.FromResult<Account?>(null));

            var command = new DeleteAccountRequest { AccountId = accountId };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.AccountRepository.Received(1).Delete(accountId);
            await _unitOfWorkMock.DidNotReceive().Save();
        }
    }
}