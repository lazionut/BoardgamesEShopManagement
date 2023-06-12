using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveBoardgame
{
    public class ArchiveBoardgameRequestHandler : IRequestHandler<ArchiveBoardgameRequest, Boardgame?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveBoardgameRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Boardgame?> Handle(ArchiveBoardgameRequest request, CancellationToken cancellationToken)
        {
            Boardgame? searchedBoardgame = await _unitOfWork.BoardgameRepository.GetById(request.BoardgameId);

            if (searchedBoardgame == null)
            {
                return null;
            }

            searchedBoardgame.Price = 0;
            searchedBoardgame.Quantity = 0;
            searchedBoardgame.IsArchived = true;

            searchedBoardgame.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Save();

            return searchedBoardgame;
        }
    }
}