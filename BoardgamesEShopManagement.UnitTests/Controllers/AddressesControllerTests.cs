using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount;
using BoardgamesEShopManagement.Application.Addresses.Commands.UpdateAddress;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Controllers
{
    public class AddressesControllerTests : CustomControllerBaseMock
    {
        private readonly IMediator _mediatorMock;
        private readonly AddressesController _controller;

        public AddressesControllerTests()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _controller = new AddressesController(_mediatorMock)
            {
                ControllerContext = ControllerContext
            };
        }

        [Fact]
        public async Task UpdateAddress_ValidAddress_ReturnsNoContentResult()
        {
            // Arrange
            var addressPostPutDto = new AddressPostPutDto
            {
                Details = "123 Main St",
                City = "Anytown",
                County = "Anycounty",
                Country = "Anycountry",
                Phone = "123-456-7890"
            };

            var account = new Account
            {
                Id = 1,
                AddressId = 1
            };

            var address = new Address
            {
                Id = 1,
                Details = "123 Main St",
                City = "Anytown",
                County = "Anycounty",
                Country = "Anycountry",
                Phone = "123-456-7890"
            };

            _mediatorMock.Send(Arg.Any<GetAccountQuery>()).Returns(account);
            _mediatorMock.Send(Arg.Any<UpdateAddressRequest>()).Returns(address);

            // Act
            var result = await _controller.UpdateAddress(addressPostPutDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateAddress_AddressNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var addressPostPutDto = new AddressPostPutDto
            {
                Details = "123 Main St",
                City = "Anytown",
                County = "Anycounty",
                Country = "Anycountry",
                Phone = "123-456-7890"
            };

            var account = new Account
            {
                Id = 1,
                AddressId = 1
            };

            _mediatorMock.Send(Arg.Any<GetAccountQuery>()).Returns(account);
            _mediatorMock.Send(Arg.Any<UpdateAddressRequest>())!.Returns((Address)null);

            // Act
            var result = await _controller.UpdateAddress(addressPostPutDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}