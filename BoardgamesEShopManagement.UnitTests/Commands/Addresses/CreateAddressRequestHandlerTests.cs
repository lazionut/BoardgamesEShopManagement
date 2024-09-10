using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Addresses.Commands.CreateAddress;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Addresses
{
    public class CreateAddressRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly CreateAddressRequestHandler _handler;

        public CreateAddressRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new CreateAddressRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldCreateAddress_WhenRequestIsValid()
        {
            // Arrange
            var expectedAddress = new Address
            {
                Details = "123 Main St",
                City = "Sample City",
                County = "Sample County",
                Country = "Sample Country",
                Phone = "1234567890",
                IsArchived = false
            };

            _unitOfWorkMock.AddressRepository.Create(Arg.Any<Address>()).Returns(Task.CompletedTask);
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            var command = new CreateAddressRequest
            {
                AddressDetails = expectedAddress.Details,
                AddressCity = expectedAddress.City,
                AddressCounty = expectedAddress.County,
                AddressCountry = expectedAddress.Country,
                AddressPhone = expectedAddress.Phone
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
            Assert.False(actualResult.IsArchived);

            await _unitOfWorkMock.AddressRepository.Received(1).Create(Arg.Any<Address>());
            await _unitOfWorkMock.Received(1).Save();
        }
    }
}