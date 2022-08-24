using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategory
{
    public class GetBoardgamesPerCategoryListQueryHandler : IRequestHandler<GetBoardgamesPerCategoryListQuery, List<Boardgame>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgamesPerCategoryListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Boardgame>> Handle(GetBoardgamesPerCategoryListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetBoardgamesPerCategory(request.CategoryId);
        }
    }
}
