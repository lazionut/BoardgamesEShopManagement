using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AddressPutDto
    {
        [MaxLength(200)]
        public string AddressDetails { get; set; } = null!;

        [MaxLength(50)]
        public string AddressCity { get; set; } = null!;

        [MaxLength(50)]
        public string AddressCounty { get; set; } = null!;

        [MaxLength(150)]
        public string AddressCountry { get; set; } = null!;

        [MaxLength(30)]
        public string AddressPhone { get; set; } = null!;
    }
}
