using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class ReviewPostDto
    {
        [Required]
        [MaxLength(50)]
        public string ReviewTitle { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string ReviewAuthor { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        public byte ReviewScore { get; set; }

        [MaxLength(4000)]
        public string? ReviewContent { get; set; }

        [Required]
        public int ReviewBoardgameId { get; set; }

        [Required]
        public int ReviewAccountId { get; set; }
    }
}
