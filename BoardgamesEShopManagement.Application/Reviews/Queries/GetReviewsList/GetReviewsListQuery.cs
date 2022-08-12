using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsList
{
    public class GetReviewsListQuery : IRequest<IEnumerable<ReviewListVm>>
    {

    }
}
