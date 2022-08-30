using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountEmailPatchDto
    {
        [Required]
        [MaxLength(255)]
        public string AccountEmail { get; set; } = null!;
    }
}
