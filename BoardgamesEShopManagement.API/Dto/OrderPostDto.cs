using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class OrderPostDto
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public List<int> BoardgameIds { get; set; } = null!;

        [Required]
        public List<int> BoardgameQuantities { get; set; } = null!;
    }
}
