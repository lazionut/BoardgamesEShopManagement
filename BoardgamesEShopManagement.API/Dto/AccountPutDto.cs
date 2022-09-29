using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountPutDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = null!;
    }
}
