﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.CreateAddress
{
    public class CreateAddressRequestHandler : IRequestHandler<CreateAddressRequest, Address>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAddressRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Address> Handle(CreateAddressRequest request, CancellationToken cancellationToken)
        {
            Address address = new Address
            {
                Details = request.AddressDetails,
                City = request.AddressCity,
                County = request.AddressCounty,
                Country = request.AddressCountry,
                Phone = request.AddressPhone,
                IsArchived = false
            };

            await _unitOfWork.AddressRepository.Create(address);
            await _unitOfWork.Save();

            return address;
        }
    }
}
