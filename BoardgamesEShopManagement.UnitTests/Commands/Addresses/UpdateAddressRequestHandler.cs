using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Addresses.Commands.UpdateAddress;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Addresses
{
    public class UpdateAddressRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly UpdateAddressRequestHandler _handler;

        public UpdateAddressRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new UpdateAddressRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldUpdateAddress_WhenRequestIsValid()
        {
            // Arrange
            var existingAddress = new Address
            {
                Id = 1,
                Details = "Old Details",
                City = "Old City",
                County = "Old County",
                Country = "Old Country",
                Phone = "0987654321",
                IsArchived = false
            };

            var updatedAddress = new Address
            {
                Id = 1,
                Details = "New Details",
                City = "New City",
                County = "New County",
                Country = "New Country",
                Phone = "1234567890",
                IsArchived = false
            };

            _unitOfWorkMock.AddressRepository.GetById(existingAddress.Id)!.Returns(Task.FromResult(existingAddress));
            _unitOfWorkMock.AddressRepository.Update(Arg.Any<Address>());
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            var command = new UpdateAddressRequest
            {
                AddressId = existingAddress.Id,
                AddressDetails = updatedAddress.Details,
                AddressCity = updatedAddress.City,
                AddressCounty = updatedAddress.County,
                AddressCountry = updatedAddress.Country,
                AddressPhone = updatedAddress.Phone
            };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(command.AddressDetails, actualResult.Details);
            Assert.Equal(command.AddressCity, actualResult.City);
            Assert.Equal(command.AddressCounty, actualResult.County);
            Assert.Equal(command.AddressCountry, actualResult.Country);
            Assert.Equal(command.AddressPhone, actualResult.Phone);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAddressDoesNotExist()
        {
            // Arrange
            _unitOfWorkMock.AddressRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult<Address?>(null));

            var command = new UpdateAddressRequest
            {
                AddressId = 1,
                AddressDetails = "New Details",
                AddressCity = "New City",
                AddressCounty = "New County",
                AddressCountry = "New Country",
                AddressPhone = "1234567890"
            };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.Null(actualResult);
        }
    }
}