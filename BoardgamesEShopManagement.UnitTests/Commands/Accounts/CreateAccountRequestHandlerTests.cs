using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Accounts.Commands.CreateAccount;
using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Accounts
{
    public class CreateAccountRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly UserManager<Account> _userManagerMock;
        private readonly CreateAccountRequestHandler _handler;

        public CreateAccountRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _userManagerMock = Substitute.For<UserManager<Account>>(
                Substitute.For<IUserStore<Account>>(), null, null, null, null, null, null, null, null);
            _handler = new CreateAccountRequestHandler(_unitOfWorkMock, _userManagerMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountAlreadyExistsByEmail()
        {
            // Arrange
            var command = new CreateAccountRequest
            {
                AccountEmail = "existing@example.com",
                AccountFirstName = "John",
                AccountLastName = "Doe",
                AccountPassword = "Password123!",
                AccountAddressId = 1
            };

            _unitOfWorkMock.AccountRepository.GetByEmail(command.AccountEmail)!.Returns(Task.FromResult(new Account()));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Null(result);
            await _unitOfWorkMock.AccountRepository.Received(1).GetByEmail(command.AccountEmail);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAddressDoesNotExist()
        {
            // Arrange
            var command = new CreateAccountRequest
            {
                AccountEmail = "new@example.com",
                AccountFirstName = "John",
                AccountLastName = "Doe",
                AccountPassword = "Password123!",
                AccountAddressId = 1
            };

            _unitOfWorkMock.AccountRepository.GetByEmail(command.AccountEmail).Returns(Task.FromResult<Account?>(null));
            _unitOfWorkMock.AddressRepository.GetById(command.AccountAddressId).Returns(Task.FromResult<Address?>(null));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.AccountRepository.Received(1).GetByEmail(command.AccountEmail);
            await _unitOfWorkMock.AddressRepository.Received(1).GetById(command.AccountAddressId);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountAlreadyExistsByAddressId()
        {
            // Arrange
            var command = new CreateAccountRequest
            {
                AccountEmail = "new@example.com",
                AccountFirstName = "John",
                AccountLastName = "Doe",
                AccountPassword = "Password123!",
                AccountAddressId = 1
            };

            _unitOfWorkMock.AccountRepository.GetByEmail(command.AccountEmail).Returns(Task.FromResult<Account?>(null));
            _unitOfWorkMock.AddressRepository.GetById(command.AccountAddressId)!.Returns(Task.FromResult(new Address()));
            _unitOfWorkMock.AccountRepository.GetByAddressId(command.AccountAddressId)!.Returns(Task.FromResult(new Account()));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Null(result);

            await _unitOfWorkMock.AccountRepository.Received(1).GetByEmail(command.AccountEmail);
            await _unitOfWorkMock.AddressRepository.Received(1).GetById(command.AccountAddressId);
            await _unitOfWorkMock.AccountRepository.Received(1).GetByAddressId(command.AccountAddressId);
        }

        [Fact]
        public async Task Handle_ShouldCreateAccount_WhenValidRequest()
        {
            // Arrange
            var command = new CreateAccountRequest
            {
                AccountEmail = "new@example.com",
                AccountFirstName = "John",
                AccountLastName = "Doe",
                AccountPassword = "Password123!",
                AccountAddressId = 1
            };

            _unitOfWorkMock.AccountRepository.GetByEmail(command.AccountEmail).Returns(Task.FromResult<Account?>(null));
            _unitOfWorkMock.AddressRepository.GetById(command.AccountAddressId)!.Returns(Task.FromResult(new Address()));
            _unitOfWorkMock.AccountRepository.GetByAddressId(command.AccountAddressId).Returns(Task.FromResult<Account?>(null));
            _userManagerMock.CreateAsync(Arg.Any<Account>(), command.AccountPassword).Returns(IdentityResult.Success);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.AccountFirstName, result.FirstName);
            Assert.Equal(command.AccountLastName, result.LastName);
            Assert.Equal(command.AccountEmail, result.Email);
            Assert.Equal(command.AccountAddressId, result.AddressId);
            Assert.False(result.IsArchived);
            await _unitOfWorkMock.AccountRepository.Received(1).GetByEmail(command.AccountEmail);
            await _unitOfWorkMock.AddressRepository.Received(1).GetById(command.AccountAddressId);
            await _unitOfWorkMock.AccountRepository.Received(1).GetByAddressId(command.AccountAddressId);
            await _userManagerMock.Received(1).CreateAsync(Arg.Any<Account>(), command.AccountPassword);
        }
    }
}