using MediatR;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategoryCounter
{
    public class GetBoardgamesListPerCategoryQueryCounterHandler : IRequestHandler<GetBoardgamesListPerCategoryCounterQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgamesListPerCategoryQueryCounterHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetBoardgamesListPerCategoryCounterQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetPerCategoryCounter(request.CategoryId);
        }
    }
}
