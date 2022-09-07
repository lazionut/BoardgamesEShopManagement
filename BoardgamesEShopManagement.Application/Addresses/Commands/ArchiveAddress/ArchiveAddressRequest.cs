using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveAddress
{
    public class ArchiveAddressRequest : IRequest<Address>
    {
        public int AddressId { get; set; }
    }
}
