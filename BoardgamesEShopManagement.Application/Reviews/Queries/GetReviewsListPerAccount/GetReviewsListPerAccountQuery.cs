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