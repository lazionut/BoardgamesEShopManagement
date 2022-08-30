using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountPasswordPatchDto
    {
        [Required]
        [MaxLength(128)]
        public string AccountPassword { get; set; } = null!;
    }
}
