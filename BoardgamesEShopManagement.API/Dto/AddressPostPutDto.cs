using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AddressPostPutDto
    {
        [Required]
        [MaxLength(200)]
        public string Details { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string County { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; } = null!;
    }
}