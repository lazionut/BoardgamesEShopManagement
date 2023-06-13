using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.ArchiveAddress
{
    public class ArchiveAddressRequest : IRequest<Address>
    {
        public int AddressId { get; set; }
    }
}