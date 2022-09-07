using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<Category>
    {
        public int CategoryId { get; set; }
    }
}