using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountPasswordPatchDto
    {
        [Required]
        [MaxLength(128)]
        public string Password { get; set; } = null!;
    }
}
