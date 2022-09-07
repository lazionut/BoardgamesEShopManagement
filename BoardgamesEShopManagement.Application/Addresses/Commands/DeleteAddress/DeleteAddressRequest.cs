using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressRequest : IRequest<Address>
    {
        public int AddressId { get; set; }
    }
}
