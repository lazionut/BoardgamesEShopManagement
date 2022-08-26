using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountPutDto
    {
        [MaxLength(50)]
        public string AccountFirstName { get; set; } = null!;

        [MaxLength(50)]
        public string AccountLastName { get; set; } = null!;

        [MaxLength(255)]
        public string AccountEmail { get; set; } = null!;

        [MaxLength(128)]
        public string AccountPassword { get; set; } = null!;
    }
}
