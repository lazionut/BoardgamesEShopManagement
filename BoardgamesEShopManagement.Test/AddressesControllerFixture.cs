﻿using Xunit;
using MediatR;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Controllers;
using BoardgamesEShopManagement.Application.Addresses.Commands.UpdateAddress;
using BoardgamesEShopManagement.API.Dto;

namespace BoardgamesEShopManagement.Test
{
    public class AddressesControllerFixture
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        /*
         * address is now automatically created when creating an account
         * 
        [Fact]
        public async void Create_Address_CreateAddressCommandIsCalled()
        {
            CreateAddressRequest createAddressCommand = new CreateAddressRequest
            {
                AddressDetails = "CreatedDetails",
                AddressCity = "CreatedCity",
                AddressCounty = "CreatedCounty",
                AddressCountry = "CreatedCountry",
                AddressPhone = "0765123456"
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<CreateAddressRequest>(s => s == createAddressCommand), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                      new Address
                      {
                          Details = "CreatedDetails",
                          City = "CreatedCity",
                          County = "CreatedCounty",
                          Country = "CreatedCountry",
                          Phone = "0765123456"
                      }
                );

            _mockMapper
                .Setup(m => m.Map<AddressGetDto>(It.IsAny<Address>()))
                .Returns(new AddressGetDto
                {
                    Details = "CreatedDetails",
                    City = "CreatedCity",
                    County = "CreatedCounty",
                    Country = "CreatedCountry",
                    Phone = "0765123456"
                });

            AddressesController controller = new AddressesController(_mockMediator.Object, _mockMapper.Object);

            IActionResult result = await controller.CreateAddress(new AddressPostPutDto
            {
                Details = "CreatedDetails",
                City = "CreatedCity",
                County = "CreatedCounty",
                Country = "CreatedCountry",
                Phone = "0765123456"
            });

            CreatedAtActionResult okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(createAddressCommand.AddressDetails, ((AddressGetDto)okResult.Value).Details);
        }
        */

        /*
        [Fact]
        public async void Update_Address_UpdateAddressCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdateAddressRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Address
                {
                    Details = "UpdatedDetails",
                    City = "UpdatedDetails",
                    County = "UpdatedCounty",
                    Country = "UpdatedCountry",
                    Phone = "0711111111"
                });

            AddressesController controller = new AddressesController(_mockMediator.Object, _mockMapper.Object, _mockSingleton.Object);

            IActionResult result = await controller.UpdateAddress(new AddressPostPutDto
            {
                Details = "UpdatedDetails",
                City = "UpdatedDetails",
                County = "UpdatedCounty",
                Country = "UpdatedCountry",
                Phone = "0711111111"
            });

            NoContentResult noContentResult = Assert.IsType<NoContentResult>(result);

            Assert.Equal((int)HttpStatusCode.NoContent, noContentResult.StatusCode);
        }
        */

        /*
         * both controllers logic was moved to deleting and archiving account
         * 
        [Fact]
        public async void Delete_Address_DeleteAddressCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.Is<GetAddressQuery>(s => s.AddressId == 1), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Address() { });

            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteAddressRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Address
                {
                    Details = "812 Joanny Overpass",
                    City = "Ziemannport",
                    County = "Borders",
                    Country = "Madagascar",
                    Phone = "948-435-5898 x22135"
                });

            AddressesController controller = new AddressesController(_mockMediator.Object, _mockMapper.Object);

            await controller.DeleteAddress(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteAddressRequest>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void Archive_Address_ArchiveAddressCommandIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.Is<ArchiveAddressRequest>(s => s.AddressId == 1), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Address() { });

            _mockMediator
                .Setup(m => m.Send(It.IsAny<ArchiveAddressRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Address
                {
                    Details = "812 Joanny Overpass",
                    City = "Ziemannport",
                    County = "Borders",
                    Country = "Madagascar",
                    Phone = "948-435-5898 x22135",
                    IsArchived = true
                });

            AddressesController controller = new AddressesController(_mockMediator.Object, _mockMapper.Object);

            await controller.ArchiveAddress(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<ArchiveAddressRequest>(), It.IsAny<CancellationToken>()), Times.Once());
        }
        */
    }
}
