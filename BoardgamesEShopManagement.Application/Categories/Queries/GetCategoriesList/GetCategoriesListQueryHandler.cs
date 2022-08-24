using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoriesListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Category>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CategoryRepository.GetAll();
        }
    }
}
