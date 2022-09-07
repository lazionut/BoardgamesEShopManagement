using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Category : EntityBase
    {
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public ICollection<Boardgame> Boardgames { get; set; } = null!;
    }
}
