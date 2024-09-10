using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Accounts
{
    public class UpdateAccountRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly UpdateAccountRequestHandler _handler;

        public UpdateAccountRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new UpdateAccountRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            var command = new UpdateAccountRequest
            {
                AccountId = 1,
                AccountFirstName = "John",
                AccountLastName = "Doe",
                AccountEmail = "john.doe@example.com"
            };

            _unitOfWorkMock.AccountRepository.GetById(command.AccountId).Returns(Task.FromResult<Account?>(null));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.AccountRepository.Received(1).GetById(command.AccountId);
            _unitOfWorkMock.AccountRepository.DidNotReceive().Update(Arg.Any<Account>());
            await _unitOfWorkMock.DidNotReceive().Save();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenEmailIsAlreadyTaken()
        {
            // Arrange
            var existingAccount = new Account
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            var anotherAccountWithSameEmail = new Account
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com"
            };

            var command = new UpdateAccountRequest
            {
                AccountId = 1,
                AccountFirstName = "John",
                AccountLastName = "Doe",
                AccountEmail = "jane.smith@example.com"
            };

            _unitOfWorkMock.AccountRepository.GetById(command.AccountId).Returns(Task.FromResult<Account?>(existingAccount));
            _unitOfWorkMock.AccountRepository.GetByEmail(command.AccountEmail).Returns(Task.FromResult<Account?>(anotherAccountWithSameEmail));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.AccountRepository.Received(1).GetById(command.AccountId);
            await _unitOfWorkMock.AccountRepository.Received(1).GetByEmail(command.AccountEmail);
            _unitOfWorkMock.AccountRepository.DidNotReceive().Update(Arg.Any<Account>());
            await _unitOfWorkMock.DidNotReceive().Save();
        }

        [Fact]
        public async Task Handle_ShouldUpdateAccount_WhenRequestIsValid()
        {
            // Arrange
            var existingAccount = new Account
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            var command = new UpdateAccountRequest
            {
                AccountId = 1,
                AccountFirstName = "John",
                AccountLastName = "Doe",
                AccountEmail = "john.new@example.com"
            };

            _unitOfWorkMock.AccountRepository.GetById(command.AccountId).Returns(Task.FromResult<Account?>(existingAccount));
            _unitOfWorkMock.AccountRepository.GetByEmail(command.AccountEmail).Returns(Task.FromResult<Account?>(null));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.AccountFirstName, result.FirstName);
            Assert.Equal(command.AccountLastName, result.LastName);
            Assert.Equal(command.AccountEmail, result.Email);
            Assert.Equal(existingAccount.UpdatedAt, result.UpdatedAt);

            await _unitOfWorkMock.AccountRepository.Received(1).GetById(command.AccountId);
            await _unitOfWorkMock.AccountRepository.Received(1).GetByEmail(command.AccountEmail);
            _unitOfWorkMock.AccountRepository.Received(1).Update(existingAccount);
            await _unitOfWorkMock.Received(1).Save();
        }
    }
}