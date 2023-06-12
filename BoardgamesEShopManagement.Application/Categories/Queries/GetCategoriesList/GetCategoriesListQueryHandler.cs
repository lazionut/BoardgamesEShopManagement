using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<Category>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoriesListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Category>?> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            int countCategories = await _unitOfWork.CategoryRepository.GetCategoryCounter();
            return await _unitOfWork.CategoryRepository.GetAll(1, countCategories);
        }
    }
}