using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByName
{
    public class GetBoardgamesListByNameQueryHandler : IRequestHandler<GetBoardgamesListByNameQuery, List<Boardgame>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgamesListByNameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Boardgame>?> Handle(GetBoardgamesListByNameQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetPerName
                (request.BoardgameNameCharacters, request.BoardgamePageIndex,
                request.BoardgamePageSize, request.BoardgameSortOrder);
        }
    }
}