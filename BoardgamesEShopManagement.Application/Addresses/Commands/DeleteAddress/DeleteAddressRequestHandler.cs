using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressRequestHandler : IRequestHandler<DeleteAddressRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAddressRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
        {
            bool isAddressDeleted = await _unitOfWork.AddressRepository.Delete(request.AddressId);

            await _unitOfWork.Save();

            return isAddressDeleted;
        }
    }
}
