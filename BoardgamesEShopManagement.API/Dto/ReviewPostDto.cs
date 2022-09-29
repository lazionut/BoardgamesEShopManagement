using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class ReviewPostDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Author { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        public byte Score { get; set; }

        [MaxLength(4000)]
        public string Content { get; set; } = null!;

        [Required]
        public int BoardgameId { get; set; }
    }
}
