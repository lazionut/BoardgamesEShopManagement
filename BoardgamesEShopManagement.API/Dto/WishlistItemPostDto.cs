using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class WishlistItemPostDto
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public int WishlistId { get; set; }

        [Required]
        public int BoardgameId { get; set; }
    }
}
