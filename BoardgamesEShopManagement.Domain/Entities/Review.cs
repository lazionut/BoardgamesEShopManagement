using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Review : EntityBase
    {
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        [MaxLength(100)]
        public string Author { get; set; } = null!;

        [Column(TypeName = "tinyint")]
        [Range(1, 5)]
        public byte Score { get; set; }

        [MaxLength(4000)]
        public string Content { get; set; } = null!;
        public Boardgame Boardgame { get; set; } = null!;
        public int BoardgameId { get; set; }
        public Account Account { get; set; } = null!;
        public int AccountId { get; set; }
    }
}
