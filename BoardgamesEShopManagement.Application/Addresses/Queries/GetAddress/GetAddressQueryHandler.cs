using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Addresses.Queries.GetAddress
{
    public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, Address>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAddressQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Address> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AddressRepository.GetById(request.AddressId);
        }
    }
}
