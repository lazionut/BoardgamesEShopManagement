using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame
{
    public class DeleteBoardgameRequestHandler : IRequestHandler<DeleteBoardgameRequest, bool>
    {
        private readonly IBoardgameRepository _boardgameRepository;
        public DeleteBoardgameRequestHandler(IBoardgameRepository boardgameRepository)
        {
            _boardgameRepository = boardgameRepository;
        }
        public Task<bool> Handle(DeleteBoardgameRequest request, CancellationToken cancellationToken)
        {
            bool isDeleted = _boardgameRepository.Delete(request.BoardgameId);

            return Task.FromResult(isDeleted);
        }
    }
}
