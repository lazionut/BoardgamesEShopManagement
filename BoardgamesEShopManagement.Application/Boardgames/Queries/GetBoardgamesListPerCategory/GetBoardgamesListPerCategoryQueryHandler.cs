using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategory
{
    public class GetBoardgamesListPerCategoryQueryHandler : IRequestHandler<GetBoardgamesListPerCategoryQuery, List<Boardgame>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgamesListPerCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Boardgame>?> Handle(GetBoardgamesListPerCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetPerCategory
                (request.CategoryId, request.BoardgamePageIndex, request.BoardgamePageSize, request.BoardgameSortOrder);
        }
    }
}