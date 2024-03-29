﻿using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.ArchiveAddress
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

            searchedAddress.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Save();

            return searchedAddress;
        }
    }
}