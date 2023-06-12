using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressRequest : IRequest<Address>
    {
        public int AddressId { get; set; }
    }
}