using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountPostDto
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
        [RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$")]
        [MaxLength(30)]
        public string Phone { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        [MaxLength(255)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(128)]
        public string Password { get; set; } = null!;

        [Required]
        public bool IsAdmin { get; set; }
    }
}
