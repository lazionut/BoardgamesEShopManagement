using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewRequest : IRequest<Review>
    {
        public int ReviewId { get; set; }
    }
}
