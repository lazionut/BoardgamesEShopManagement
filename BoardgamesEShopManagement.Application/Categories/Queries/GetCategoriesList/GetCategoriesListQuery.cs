using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<List<Category>>
    {

    }
}