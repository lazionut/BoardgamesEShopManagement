using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressRequestHandler : IRequestHandler<UpdateAddressRequest, Address>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAddressRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Address> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
        {
            Address updatedAddress = await _unitOfWork.AddressRepository.GetById(request.AddressId);

            if (updatedAddress == null)
            {
                return null;
            }

            updatedAddress.Details = request.AddressDetails;
            updatedAddress.City = request.AddressCity;
            updatedAddress.County = request.AddressCounty;
            updatedAddress.Country = request.AddressCountry;
            updatedAddress.Phone = request.AddressPhone;

            await _unitOfWork.AddressRepository.Update(updatedAddress);

            await _unitOfWork.Save();

            return updatedAddress;
        }
    }
}
