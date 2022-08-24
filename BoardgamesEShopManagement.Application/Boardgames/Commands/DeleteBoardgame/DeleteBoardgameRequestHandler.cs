using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame
{
    public class DeleteBoardgameRequestHandler : IRequestHandler<DeleteBoardgameRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBoardgameRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBoardgameRequest request, CancellationToken cancellationToken)
        {
            bool isBoardgameDeleted = await _unitOfWork.BoardgameRepository.Delete(request.BoardgameId);

            await _unitOfWork.Save();

            return isBoardgameDeleted;
        }
    }
}
