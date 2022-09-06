using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class BoardgamePostPutDto
    {
        public string? BoardgameImage { get; set; }

        [Required]
        [MaxLength(50)]
        public string BoardgameName { get; set; } = null!;

        [Required]
        [Range(-3500, 9999)]
        public int BoardgameReleaseYear { get; set; }

        [MaxLength(4000)]
        public string? BoardgameDescription { get; set; }

        [Required]
        [Range(0.5, double.PositiveInfinity)]
        public decimal BoardgamePrice { get; set; }

        [MaxLength(2000)]
        public string? BoardgameLink { get; set; }

        [Range(1, int.MaxValue)]
        public int BoardgameQuantity { get; set; }

        [Required]
        public int BoardgameCategoryId { get; set; }
    }
}
