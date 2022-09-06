using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerAccount
{
    public class GetReviewsListPerAccountQuery : IRequest<List<Review>>
    {
        public int ReviewAccountId { get; set; }
        public int ReviewPageIndex { get; set; }
        public int ReviewPageSize { get; set; }
    }
}