using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame
{
    public class GetBoardgameQueryHandler : IRequestHandler<GetBoardgameQuery, Boardgame?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Boardgame?> Handle(GetBoardgameQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetById(request.BoardgameId);
        }
    }
}