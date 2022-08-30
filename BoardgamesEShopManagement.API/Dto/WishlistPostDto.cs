using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class WishlistPostDto
    {
        [Required]
        [MaxLength(50)]
        public string WishlistName { get; set; } = null!;

        [Required]
        public int WishlistAccountId { get; set; }

        [Required]
        public List<int> WishlistBoardgameIds { get; set; } = null!;
    }
}
