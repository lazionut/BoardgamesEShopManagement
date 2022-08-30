using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class CategoryPostPutDto
    {
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; } = null!;
    }
}
