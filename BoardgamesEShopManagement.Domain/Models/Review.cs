using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Models
{
    public class Review
    {
        public int CategoryId { get; set; }
        public int ReviewId { get; set; }
        public string ReviewAuthor { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewContent { get; set; }
        public DateTime ReviewDate { get; set; }

    }
}
