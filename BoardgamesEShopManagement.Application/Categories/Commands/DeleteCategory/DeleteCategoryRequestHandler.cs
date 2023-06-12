using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, Category?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category?> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            Category? deletedCategory = await _unitOfWork.CategoryRepository.Delete(request.CategoryId);

            if (deletedCategory == null)
            {
                return null;
            }

            await _unitOfWork.Save();

            return deletedCategory;
        }
    }
}