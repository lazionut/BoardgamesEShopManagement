using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class ReviewPatchDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        [MaxLength(4000)]
        public string Content { get; set; } = null!;
    }
}
