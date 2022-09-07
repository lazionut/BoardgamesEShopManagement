using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryRequest : IRequest<Category>
    {
        public string CategoryName { get; set; } = null!;
    }
}
