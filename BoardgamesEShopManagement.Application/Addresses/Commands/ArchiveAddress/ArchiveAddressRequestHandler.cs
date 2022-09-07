using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Utils;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveAddress
{
    public class ArchiveAddressRequestHandler : IRequestHandler<ArchiveAddressRequest, Address?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveAddressRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Address?> Handle(ArchiveAddressRequest request, CancellationToken cancellationToken)
        {
            Address? searchedAddress = await _unitOfWork.AddressRepository.GetById(request.AddressId);

            if (searchedAddress == null)
            {
                return null;
            }

            searchedAddress.Details = "Anonymized";
            searchedAddress.City = "Anonymized";
            searchedAddress.County = "Anonymized";
            searchedAddress.Country = "Anonymized";
            searchedAddress.Phone = "Anonymized";

            searchedAddress.UpdatedAt = DateTimeUtils.GetCurrentDateTimeWithoutMiliseconds();

            await _unitOfWork.Save();

            return searchedAddress;
        }
    }
}
