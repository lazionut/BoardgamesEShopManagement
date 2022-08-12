using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Review : EntityBase
    {
        public int BoardgameId { get; init; }
        public string ReviewTitle { get; set; }
        public string ReviewAuthor { get; init; }
        public string ReviewContent { get; set; }
    }
}
