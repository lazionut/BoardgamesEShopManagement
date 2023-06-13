using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, Category?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category?> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            Category? updatedCategory = await _unitOfWork.CategoryRepository.GetById(request.CategoryId);

            if (updatedCategory == null)
            {
                return null;
            }

            updatedCategory.Name = request.CategoryName;
            updatedCategory.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.CategoryRepository.Update(updatedCategory);
            await _unitOfWork.Save();

            return updatedCategory;
        }
    }
}