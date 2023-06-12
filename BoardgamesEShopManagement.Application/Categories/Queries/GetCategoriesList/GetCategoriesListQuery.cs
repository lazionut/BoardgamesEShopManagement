using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<List<Category>>
    {
    }
}