using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList
{
    public class GetBoardgameListQueryHandler : IRequestHandler<GetBoardgamesListQuery, List<Boardgame>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgameListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Boardgame>> Handle(GetBoardgamesListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetAll(request.BoardgamePageIndex, request.BoardgamePageSize);
        }
    }
}
