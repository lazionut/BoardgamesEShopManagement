using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Review : EntityBase
    {
        [MaxLength(50)]
        public string ReviewTitle { get; set; } = null!;

        [MaxLength(50)]
        public string ReviewAuthor { get; set; } = null!;

        [MaxLength(4000)]
        public string ReviewContent { get; set; } = null!;
        public Boardgame Boardgame { get; set; } = null!;
        public int BoardgameId { get; set; }
    }
}
