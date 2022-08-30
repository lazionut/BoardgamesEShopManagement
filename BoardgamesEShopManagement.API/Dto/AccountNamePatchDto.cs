using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountNamePatchDto
    {
        [Required]
        [MaxLength(50)]
        public string AccountFirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string AccountLastName { get; set; } = null!;
    }
}
