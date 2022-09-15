using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountEmailPatchDto
    {
        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = null!;
    }
}
