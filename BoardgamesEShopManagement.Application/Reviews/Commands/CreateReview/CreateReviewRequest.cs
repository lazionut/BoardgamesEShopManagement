using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewRequest : IRequest<int>
    {
        public int BoardgameId { get; set; }
        public string ReviewTitle { get; set; } = null!;
        public string ReviewAuthor { get; set; } = null!;
        public string ReviewContent { get; set; } = null!;
    }
}
