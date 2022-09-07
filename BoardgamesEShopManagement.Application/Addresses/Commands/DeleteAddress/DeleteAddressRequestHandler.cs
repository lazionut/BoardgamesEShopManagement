using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressRequestHandler : IRequestHandler<DeleteAddressRequest, Address>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAddressRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Address> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
        {
            Address deletedAddress = await _unitOfWork.AddressRepository.Delete(request.AddressId);

            if (deletedAddress == null)
            {
                return null;
            }

            await _unitOfWork.Save();

            return deletedAddress;
        }
    }
}
