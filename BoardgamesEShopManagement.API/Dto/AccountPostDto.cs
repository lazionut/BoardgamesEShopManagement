using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountPostDto
    {
        [Required]
        [MaxLength(50)]
        public string AccountFirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string AccountLastName { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string AccountEmail { get; set; } = null!;

        [Required]
        [MaxLength(128)]
        public string AccountPassword { get; set; } = null!;

        [Required]
        public int AccountAddressId { get; set; }
    }
}
