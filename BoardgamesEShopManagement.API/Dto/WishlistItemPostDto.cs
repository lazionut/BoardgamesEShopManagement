using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class WishlistItemPostDto
    {
        [Required]
        public int WishlistAccountId { get; set; }

        [Required]
        public int WishlistId { get; set; }

        [Required]
        public int WishlistBoardgameId { get; set; }
    }
}
