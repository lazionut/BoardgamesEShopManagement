using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class ReviewPostDto
    {
        [MaxLength(50)]
        public string ReviewTitle { get; set; } = null!;

        [MaxLength(100)]
        public string ReviewAuthor { get; set; } = null!;

        [Range(1, 5)]
        public byte ReviewScore { get; set; }

        [MaxLength(4000)]
        public string ReviewContent { get; set; } = null!;
        public int ReviewBoardgameId { get; set; }
        public int ReviewAccountId { get; set; }
    }
}
