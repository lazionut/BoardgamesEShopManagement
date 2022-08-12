using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgame
{
    public class GetReviewsListPerBoardgameQuery : IRequest<IEnumerable<ReviewsListPerBoardgameVm>>
    {
        public int BoardgameId { get; set; }
    }
}