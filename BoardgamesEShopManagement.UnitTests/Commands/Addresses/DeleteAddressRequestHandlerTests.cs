using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Addresses.Commands.DeleteAddress;
using BoardgamesEShopManagement.Domain.Entities;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Commands.Addresses
{
    public class DeleteAddressRequestHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly DeleteAddressRequestHandler _handler;

        public DeleteAddressRequestHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new DeleteAddressRequestHandler(_unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnDeletedAddress_WhenAddressExists()
        {
            // Arrange
            var addressId = 1;
            var expectedAddress = new Address { Id = addressId, Details = "123 Main St" };

            _unitOfWorkMock.AddressRepository.Delete(addressId).Returns(Task.FromResult<Address?>(expectedAddress));
            _unitOfWorkMock.Save().Returns(Task.CompletedTask);

            var command = new DeleteAddressRequest { AddressId = addressId };

            // Act
            var actualResult = _handler.Handle(command, default);

            // Assert
            Assert.NotNull(actualResult);

            await _unitOfWorkMock.AddressRepository.Received(1).Delete(addressId);
            await _unitOfWorkMock.Received(1).Save();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAddressDoesNotExist()
        {
            // Arrange
            var addressId = 1;

            _unitOfWorkMock.AddressRepository.Delete(addressId).Returns(Task.FromResult<Address?>(null));

            var command = new DeleteAddressRequest { AddressId = addressId };

            // Act
            var actualResult = await _handler.Handle(command, default);

            // Assert
            Assert.Null(actualResult);

            await _unitOfWorkMock.AddressRepository.Received(1).Delete(addressId);
            await _unitOfWorkMock.DidNotReceive().Save();
        }
    }
}