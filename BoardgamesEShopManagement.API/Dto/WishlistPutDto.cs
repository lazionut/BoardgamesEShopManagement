using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class WishlistPutDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public List<int> BoardgameIds { get; set; } = null!;
    }
}
