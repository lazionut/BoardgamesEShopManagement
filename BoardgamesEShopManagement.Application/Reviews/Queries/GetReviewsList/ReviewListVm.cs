using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsList
{
    public class ReviewListVm
    {
        public int BoardgameId { get; set; }
        public int ReviewId { get; set; }
        public string ReviewTitle { get; set; } = null!;
        public string ReviewAuthor { get; set; } = null!;
        public byte ReviewScore { get; set; } 
        public string ReviewContent { get; set; } = null!;
    }
}
