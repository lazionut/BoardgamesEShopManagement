using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<Category>
    {
        public int CategoryId { get; set; }
    }
}