using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewRequest : IRequest<Review>
    {
        public string ReviewTitle { get; set; } = null!;
        public string ReviewAuthor { get; set; } = null!;
        public byte ReviewScore { get; set; }
        public string ReviewContent { get; set; } = null!;
        public int ReviewBoardgameId { get; set; }
        public int ReviewAccountId { get; set; }
    }
}
