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
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.DeleteAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.ArchiveAccount;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistByAccount;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrderByAccount;

namespace BoardgamesEShopManagement.Test
{
    public class AccountsControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

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
                    AccountFirstName = "FirstName",
                    AccountLastName = "LastName",
                    AccountEmail = "email@example.com",
                    AccountPassword = "password",
                    AccountAddressId = 1,
                });

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.CreateAccount(new AccountPostDto
            {
                AccountFirstName = "FirstName",
                AccountLastName = "LastName",
                AccountEmail = "email@example.com",
                AccountPassword = "password",
                AccountAddressId = 1,
            });

            CreatedAtActionResult okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(createAccountCommand.AccountEmail, ((AccountGetDto)okResult.Value).AccountEmail);
        }

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
                    Password = "zbUBQlO7UZ",
                    IsArchived = false
                });

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.GetAccount(1);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async void Get_Reviews_List_Per_Account_GetReviewsListPerAccountQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetReviewsListPerAccountQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Review> {
                new Review
                {
                    Title = "This",
                    Author = "Calvin Grady",
                    Score = 3,
                    Content = "It only works when I'm Cook Islands.",
                    BoardgameId = 6,
                    AccountId = 1
                },
                });

            _mockMapper
                .Setup(m => m.Map<List<ReviewGetDto>>(It.IsAny<List<Review>>()))
                .Returns(new List<ReviewGetDto> {
                new ReviewGetDto
                {
                    ReviewTitle = "This",
                    ReviewAuthor = "Calvin Grady",
                    ReviewScore = 3,
                    ReviewContent = "It only works when I'm Cook Islands.",
                    ReviewBoardgameId = 6,
                    ReviewAccountId = 1
                },
                });

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.GetReviewsPerAccount(1, 1, 5);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async void Update_Account_Name_UpdateAccountNameCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdateAccountRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Account
                {
                    FirstName = "Nettie",
                    LastName = "Okuneva",
                    Email = "Kristoffer_Hayes@hotmail.com",
                    Password = "zbUBQlO7UZ",
                    AddressId = 1
                });

            _mockMapper
                .Setup(m => m.Map<AccountNamePatchDto>(It.IsAny<Account>()))
                .Returns(new AccountNamePatchDto
                {
                    AccountFirstName = "Updated FirstName",
                    AccountLastName = "Updated LastName"
                });

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.UpdateAccountName(1, new AccountNamePatchDto
            {
                AccountFirstName = "Updated FirstName",
                AccountLastName = "Updated LastName"
            });

            NoContentResult noContentResult = Assert.IsType<NoContentResult>(result);

            Assert.Equal((int)HttpStatusCode.NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async void Update_Account_Email_UpdateAccountEmailCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdateAccountRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Account
                {
                    FirstName = "Nettie",
                    LastName = "Okuneva",
                    Email = "Kristoffer_Hayes@hotmail.com",
                    Password = "zbUBQlO7UZ",
                    AddressId = 1
                });

            _mockMapper
                .Setup(m => m.Map<AccountEmailPatchDto>(It.IsAny<Account>()))
                .Returns(new AccountEmailPatchDto
                {
                    AccountEmail = "updatedemail@example.com",
                });

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.UpdateAccountEmail(1, new AccountEmailPatchDto
            {
                AccountEmail = "updatedemail@example.com"
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
                    Password = "zbUBQlO7UZ",
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
                    Password = "zbUBQlO7UZ",
                    IsArchived = false
                });

            AccountsController controller = new AccountsController(_mockMediator.Object, _mockMapper.Object);

            await controller.ArchiveAccount(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<ArchiveAccountRequest>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}