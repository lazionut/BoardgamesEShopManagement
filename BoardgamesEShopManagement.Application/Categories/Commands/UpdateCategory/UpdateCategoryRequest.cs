using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryRequest : IRequest<Category>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}