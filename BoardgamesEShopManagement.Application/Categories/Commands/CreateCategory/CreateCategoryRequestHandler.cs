using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, Category>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            Category category = new Category { Name = request.CategoryName };

            await _unitOfWork.CategoryRepository.Create(category);
            await _unitOfWork.Save();

            return category;
        }
    }
}
