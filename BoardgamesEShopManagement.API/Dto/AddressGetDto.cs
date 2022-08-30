using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AddressGetDto
    {
        public int AddressId { get; set; }
        public string AddressDetails { get; set; } = null!;

        public string AddressCity { get; set; } = null!;

        public string AddressCounty { get; set; } = null!;

        public string AddressCountry { get; set; } = null!;

        public string AddressPhone { get; set; } = null!;
    }
}
