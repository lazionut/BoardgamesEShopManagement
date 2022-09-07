using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryRequest : IRequest<Category>
    {
        public int CategoryId { get; set; }
    }
}
