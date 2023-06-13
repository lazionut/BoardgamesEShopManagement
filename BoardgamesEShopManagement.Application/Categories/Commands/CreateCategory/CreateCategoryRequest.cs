using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryRequest : IRequest<Category>
    {
        public string CategoryName { get; set; } = null!;
    }
}