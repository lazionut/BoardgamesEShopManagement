using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            bool isCategoryDeleted = await _unitOfWork.CategoryRepository.Delete(request.CategoryId);

            await _unitOfWork.Save();

            return isCategoryDeleted;
        }
    }
}
