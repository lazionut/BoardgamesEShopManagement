using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Addresses.Queries.GetAddress
{
    public class GetAddressQuery : IRequest<Address>
    {
        public int AddressId { get; set; }
    }
}