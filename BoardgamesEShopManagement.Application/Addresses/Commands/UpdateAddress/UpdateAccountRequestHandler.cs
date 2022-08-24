using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Categories.Commands.UpdateCategory;

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
            Address updatedAddress = await _unitOfWork.AddressRepository.UpdateAddress(request.AddressId, request.Address);

            await _unitOfWork.Save();

            return updatedAddress;
        }
    }
}
