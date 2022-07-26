﻿using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

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

            await _unitOfWork.CategoryRepository.Update(updatedCategory);
            await _unitOfWork.Save();

            return updatedCategory;
        }
    }
}
