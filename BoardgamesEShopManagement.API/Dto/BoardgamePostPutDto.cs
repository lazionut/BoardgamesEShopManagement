using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class BoardgamePostPutDto
    {
        public string? Image { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(-3500, 9999)]
        public int ReleaseYear { get; set; }

        [MaxLength(4000)]
        public string? Description { get; set; }

        [Required]
        [Range(0.5, double.PositiveInfinity)]
        public decimal Price { get; set; }

        [MaxLength(2000)]
        public string? Link { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}