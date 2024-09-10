using BoardgamesEShopManagement.Application.Abstract;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategoryCounter
{
    public class GetBoardgamesListPerCategoryCounterQueryHandler : IRequestHandler<GetBoardgamesListPerCategoryCounterQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgamesListPerCategoryCounterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetBoardgamesListPerCategoryCounterQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetPerCategoryCounter(request.CategoryId);
        }
    }
}