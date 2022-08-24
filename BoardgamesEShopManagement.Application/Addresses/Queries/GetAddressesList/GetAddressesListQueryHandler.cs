using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Addresses.Queries.GetAddressesList
{
    public class GetAddressesListQueryHandler : IRequestHandler<GetAddressesListQuery, List<Address>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAddressesListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Address>> Handle(GetAddressesListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AddressRepository.GetAll();
        }
    }
}
