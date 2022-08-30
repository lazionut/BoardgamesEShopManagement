using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class OrderPostDto
    {
        [Required]
        public int OrderAccountId { get; set; }

        [Required]
        public List<int> OrderBoardgameQuantities { get; set; } = null!;

        [Required]
        public List<int> OrderBoardgameIds { get; set; } = null!;
    }
}
