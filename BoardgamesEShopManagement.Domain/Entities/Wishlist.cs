using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Wishlist : EntityBase
    {
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public Account Account { get; set; } = null!;
        public int AccountId { get; set; }
        public ICollection<Boardgame> Boardgames { get; set; } = null!;
    }
}
