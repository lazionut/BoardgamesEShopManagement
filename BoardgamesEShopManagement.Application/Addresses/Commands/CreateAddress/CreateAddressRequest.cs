using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.CreateAddress
{
    public class CreateAddressRequest : IRequest<Address>
    {
        public string AddressDetails { get; set; } = null!;
        public string AddressCity { get; set; } = null!;
        public string AddressCounty { get; set; } = null!;
        public string AddressCountry { get; set; } = null!;
        public string AddressPhone { get; set; } = null!;
    }
}
