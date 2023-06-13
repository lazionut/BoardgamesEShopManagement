using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame
{
    public class DeleteBoardgameRequestHandler : IRequestHandler<DeleteBoardgameRequest, Boardgame?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBoardgameRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Boardgame?> Handle(DeleteBoardgameRequest request, CancellationToken cancellationToken)
        {
            Boardgame? deletedBoardgame = await _unitOfWork.BoardgameRepository.Delete(request.BoardgameId);

            if (deletedBoardgame == null)
            {
                return null;
            }

            await _unitOfWork.Save();

            return deletedBoardgame;
        }
    }
}