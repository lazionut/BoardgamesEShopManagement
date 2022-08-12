using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Commands.UpdateCategory
{
    internal class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryRequestHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<Category> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            Category updatedCategory = _categoryRepository.Update(request.CategoryId, request.Category);

            return Task.FromResult(updatedCategory);
        }
    }
}
