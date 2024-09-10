using BoardgamesEShopManagement.Application.Abstract;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList
{
    public class GetBoardgamesListCounterQueryHandler : IRequestHandler<GetBoardgamesListCounterQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgamesListCounterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetBoardgamesListCounterQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetAllSortedCounter();
        }
    }
}