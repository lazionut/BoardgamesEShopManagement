using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AddressPostPatchDto
    {
        [Required]
        [MaxLength(200)]
        public string AddressDetails { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string AddressCity { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string AddressCounty { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string AddressCountry { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string AddressPhone { get; set; } = null!;
    }
}
