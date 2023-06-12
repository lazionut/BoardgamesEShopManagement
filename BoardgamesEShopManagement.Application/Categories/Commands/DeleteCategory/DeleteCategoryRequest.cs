using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryRequest : IRequest<Category>
    {
        public int CategoryId { get; set; }
    }
}