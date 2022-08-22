using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, IEnumerable<CategoriesListVm>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesListQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<CategoriesListVm>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<CategoriesListVm> result = _categoryRepository.GetAll().Select(category => new CategoriesListVm
            {
                Id = category.Id,
                BoardgameName = category.Name,
            });

            return Task.FromResult(result);
        }
    }
}
