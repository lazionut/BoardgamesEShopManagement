using MediatR;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByNameCounter
{
    public class GetBoardgamesListByNameCounterQueryHandler : IRequestHandler<GetBoardgamesListByNameCounterQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgamesListByNameCounterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetBoardgamesListByNameCounterQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetPerNameCounter(request.BoardgameNameCharacters);
        }
    }
}
