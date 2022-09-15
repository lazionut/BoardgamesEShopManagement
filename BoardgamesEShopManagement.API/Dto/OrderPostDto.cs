using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class OrderPostDto
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public List<int> BoardgameIds { get; set; } = null!;

        [Required]
        public List<int> BoardgameQuantities { get; set; } = null!;

    }
}
