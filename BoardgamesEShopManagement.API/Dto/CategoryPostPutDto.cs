using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class CategoryPostPutDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}