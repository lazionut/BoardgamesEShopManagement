using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategory
{
    public class GetBoardgamesListPerCategoryQueryHandler : IRequestHandler<GetBoardgamesListPerCategoryQuery, List<Boardgame>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgamesListPerCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Boardgame>> Handle(GetBoardgamesListPerCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetBoardgamesPerCategory(request.CategoryId);
        }
    }
}
