using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Category?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category?> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CategoryRepository.GetById(request.CategoryId);
        }
    }
}