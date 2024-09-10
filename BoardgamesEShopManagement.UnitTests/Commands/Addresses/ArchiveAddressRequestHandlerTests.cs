using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Addresses.Commands.ArchiveAddress;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Addresses
{
    public class ArchiveAddressRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly ArchiveAddressRequestHandler _handler;

        public ArchiveAddressRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new ArchiveAddressRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldArchiveAddress_WhenAddressExists()
        {
            // Arrange
            var existingAddress = new Address
            {
                Id = 1,
                Details = "123 Main St",
                City = "Sample City",
                County = "Sample County",
                Country = "Sample Country",
                Phone = "1234567890",
                IsArchived = false
            };

            _unitOfWorkMock.AddressRepository.GetById(existingAddress.Id)!.Returns(Task.FromResult(existingAddress));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            var command = new ArchiveAddressRequest
            {
                AddressId = existingAddress.Id
            };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal("Anonymized", actualResult.Details);
            Assert.Equal("Anonymized", actualResult.City);
            Assert.Equal("Anonymized", actualResult.County);
            Assert.Equal("Anonymized", actualResult.Country);
            Assert.Equal("Anonymized", actualResult.Phone);

            await _unitOfWorkMock.AddressRepository.Received(1).GetById(existingAddress.Id);
            await _unitOfWorkMock.Received(1).Save();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAddressDoesNotExist()
        {
            // Arrange
            _unitOfWorkMock.AddressRepository.GetById(Arg.Any<int>()).Returns(Task.FromResult<Address?>(null));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            var command = new ArchiveAddressRequest
            {
                AddressId = 1
            };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.AddressRepository.Received(1).GetById(1);
            await _unitOfWorkMock.DidNotReceive().Save();
        }
    }
}