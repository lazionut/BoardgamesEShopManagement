using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Accounts.Commands.AddRoleToAccount;
using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Accounts
{
    public class AddRoleToAccountRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly UserManager<Account> _userManagerMock;
        private readonly RoleManager<IdentityRole<int>> _roleManagerMock;
        private readonly AddRoleToAccountRequestHandler _handler;

        public AddRoleToAccountRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _userManagerMock = Substitute.For<UserManager<Account>>(
                Substitute.For<IUserStore<Account>>(), null, null, null, null, null, null, null, null);
            _roleManagerMock = Substitute.For<RoleManager<IdentityRole<int>>>(
                Substitute.For<IRoleStore<IdentityRole<int>>>(), null, null, null, null);
            _handler = new AddRoleToAccountRequestHandler(_unitOfWorkMock, _userManagerMock, _roleManagerMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenAccountNotFound()
        {
            // Arrange
            var command = new AddRoleToAccountRequest { Email = "nonexistent@example.com", RoleName = "Customer" };

            _unitOfWorkMock.AccountRepository.GetByEmail(command.Email).Returns((Account)null);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_ShouldCreateRole_WhenRoleNotFound()
        {
            // Arrange
            var command = new AddRoleToAccountRequest { Email = "user@example.com", RoleName = "Customer" };
            var account = new Account { Email = command.Email };

            _unitOfWorkMock.AccountRepository.GetByEmail(command.Email).Returns(account);
            _roleManagerMock.FindByNameAsync(command.RoleName).Returns((IdentityRole<int>)null);
            _roleManagerMock.CreateAsync(Arg.Any<IdentityRole<int>>()).Returns(IdentityResult.Success);
            _userManagerMock.AddToRoleAsync(account, command.RoleName).Returns(IdentityResult.Success);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            await _roleManagerMock.Received(1).CreateAsync(Arg.Is<IdentityRole<int>>(r => r.Name == command.RoleName));
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ShouldAddRoleToAccount_WhenRoleExists()
        {
            // Arrange
            var command = new AddRoleToAccountRequest { Email = "user@example.com", RoleName = "Customer" };
            var account = new Account { Email = command.Email };
            var role = new IdentityRole<int> { Name = command.RoleName };

            _unitOfWorkMock.AccountRepository.GetByEmail(command.Email).Returns(account);
            _roleManagerMock.FindByNameAsync(command.RoleName).Returns(role);
            _userManagerMock.AddToRoleAsync(account, command.RoleName).Returns(IdentityResult.Success);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            await _userManagerMock.Received(1).AddToRoleAsync(account, command.RoleName);
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenAddRoleToAccountFails()
        {
            // Arrange
            var command = new AddRoleToAccountRequest { Email = "user@example.com", RoleName = "Customer" };
            var account = new Account { Email = command.Email };
            var role = new IdentityRole<int> { Name = command.RoleName };

            _unitOfWorkMock.AccountRepository.GetByEmail(command.Email).Returns(account);
            _roleManagerMock.FindByNameAsync(command.RoleName).Returns(role);
            _userManagerMock.AddToRoleAsync(account, command.RoleName).Returns(IdentityResult.Failed());

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.False(result);
        }
    }
}