using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListNames
{
    public class GetBoardgamesListNamesQueryHandler : IRequestHandler<GetBoardgamesListNamesQuery, List<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgamesListNamesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<string>> Handle(GetBoardgamesListNamesQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetNames();
        }
    }
}
