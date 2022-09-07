using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListSorted
{
    public class GetBoardgamesListSortedQueryHandler : IRequestHandler<GetBoardgamesListSortedQuery, List<Boardgame>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgamesListSortedQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Boardgame>> Handle(GetBoardgamesListSortedQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetBoardgamesSorted
                (request.BoardgamePageIndex, request.BoardgamePageSize, request.BoardgameSortOrder);
        }
    }
}
