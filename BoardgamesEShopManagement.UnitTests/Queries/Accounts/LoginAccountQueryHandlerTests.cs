using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Accounts.Queries.LoginAccount;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Application.Queries.Accounts
{
    public class LoginAccountQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly UserManager<Account> _userManagerMock;
        private readonly IOptions<JwtOptions> _optionsMock;
        private readonly LoginAccountQueryHandler _handler;

        public LoginAccountQueryHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _userManagerMock = Substitute.For<UserManager<Account>>(
                Substitute.For<IUserStore<Account>>(), null, null, null, null, null, null, null, null);
            _optionsMock = Substitute.For<IOptions<JwtOptions>>();
            _optionsMock.Value.Returns(new JwtOptions
            {
                Secret = "supersecretkey12345",
                ValidIssuer = "issuer",
                ValidAudience = "audience"
            });

            _handler = new LoginAccountQueryHandler(_unitOfWorkMock, _userManagerMock, _optionsMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            var accountEmail = "john.doe@example.com";
            var accountPassword = "password123";

            _unitOfWorkMock.AccountRepository.GetByEmail(accountEmail).Returns(Task.FromResult<Account?>(null));

            var query = new LoginAccountQuery { AccountEmail = accountEmail, AccountPassword = accountPassword };

            // Act
            var token = await _handler.Handle(query, default);

            // Assert
            Assert.Equal("false", token);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenPasswordIsInvalid()
        {
            // Arrange
            var accountEmail = "john.doe@example.com";
            var accountPassword = "password123";
            var account = new Account { Id = 1, Email = accountEmail };

            _unitOfWorkMock.AccountRepository.GetByEmail(accountEmail).Returns(Task.FromResult<Account?>(account));
            _userManagerMock.CheckPasswordAsync(account, accountPassword).Returns(Task.FromResult(false));

            var query = new LoginAccountQuery { AccountEmail = accountEmail, AccountPassword = accountPassword };

            // Act
            var token = await _handler.Handle(query, default);

            // Assert
            Assert.Equal("false", token);
        }
    }
}