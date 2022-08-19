using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Commands.CreateCategory
{
    internal class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, int>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryRequestHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<int> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            Category category = new Category
            {
                Id = request.CategoryId,
                Name = request.CategoryName
            };
            _categoryRepository.Create(category);

            return Task.FromResult(category.Id);
        }
    }
}
