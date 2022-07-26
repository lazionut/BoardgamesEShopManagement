﻿using Xunit;
using MediatR;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Accounts.Commands.CreateAccount;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsList;
using BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.DeleteAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.ArchiveAccount;

namespace BoardgamesEShopManagement.Test
{
    public class AccountsControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        /*
         * not MS Identity updated
         * 
        [Fact]
        public async void Create_Account_CreateAccountCommandIsCalled()
        {
            CreateAccountRequest createAccountCommand = new CreateAccountRequest
            {
                AccountFirstName = "FirstName",
                AccountLastName = "LastName",
                AccountEmail = "email@example.com",
                AccountPassword = "password",
                AccountAddressId = 1,
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<CreateAccountRequest>(s => s == createAccountCommand), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                      new Account
                      {
                          FirstName = "FirstName",
                          LastName = "LastName",
                          Email = "email@example.com",
                          Password = "password",
                          AddressId = 1,
                      }
                );

            _mockMapper
                .Setup(m => m.Map<AccountGetDto>(It.IsAny<Address>()))
                .Returns(new AccountGetDto
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Email = "email@example.com",
                    Password = "password",
                    AddressId = 1,
                });

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.CreateAccount(new AccountPostDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "email@example.com",
                Password = "password",
                AddressId = 1,
            });

            CreatedAtActionResult okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(createAccountCommand.AccountEmail, ((AccountGetDto)okResult.Value).Email);
        }
        */

        [Fact]
        public async void Get_Accounts_List_GetAccountsListQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAccountsListQuery>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            await controller.GetAccounts(1, 5);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAccountsListQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void Get_Account_GetAccountQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAccountQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                new Account
                {
                    FirstName = "Netie",
                    LastName = "Okuneva",
                    Email = "Kristoffer_Hayes@hotmail.com",
                    PasswordHash = "password_hash",
                    IsArchived = false
                });

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.GetAccount();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async void Update_Account_UpdateAccountCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdateAccountRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Account
                {
                    FirstName = "Nettie",
                    LastName = "Okuneva",
                    Email = "Kristoffer_Hayes@hotmail.com",
                    AddressId = 1
                });

            _mockMapper
                .Setup(m => m.Map<AccountPatchDto>(It.IsAny<Account>()))
                .Returns(new AccountPatchDto
                {
                    FirstName = "Updated FirstName",
                    LastName = "Updated LastName",
                    Email = "updatedemail@example.com"
                });

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.UpdateAccount(new AccountPatchDto
            {
                FirstName = "Updated FirstName",
                LastName = "Updated LastName",
                Email = "updatedemail@example.com"
            });

            NoContentResult noContentResult = Assert.IsType<NoContentResult>(result);

            Assert.Equal((int)HttpStatusCode.NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async void Delete_Account_DeleteAccountCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.Is<GetAccountQuery>(s => s.AccountId == 1), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Account() { });

            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteAccountRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Account
                {
                    FirstName = "Netie",
                    LastName = "Okuneva",
                    Email = "Kristoffer_Hayes@hotmail.com",
                    IsArchived = false
                });

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            await controller.DeleteAccount(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteAccountRequest>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void Archive_Account_ArchiveAccountCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.Is<ArchiveAccountRequest>(s => s.AccountId == 1), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Account() { });

            _mockMediator
                .Setup(m => m.Send(It.IsAny<ArchiveAccountRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Account
                {
                    FirstName = "Netie",
                    LastName = "Okuneva",
                    Email = "Kristoffer_Hayes@hotmail.com",
                    IsArchived = false
                });

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            await controller.ArchiveAccount();

            _mockMediator.Verify(x => x.Send(It.IsAny<ArchiveAccountRequest>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
