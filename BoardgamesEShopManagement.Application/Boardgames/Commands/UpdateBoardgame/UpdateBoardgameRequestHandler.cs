using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.UpdateBoardgame
{
    public class UpdateBoardgameRequestHandler : IRequestHandler<UpdateBoardgameRequest, Boardgame>
    {
        private readonly IBoardgameRepository _boardgameRepository;
        public UpdateBoardgameRequestHandler(IBoardgameRepository boardgameRepository)
        {
            _boardgameRepository = boardgameRepository;
        }
        public Task<Boardgame> Handle(UpdateBoardgameRequest request, CancellationToken cancellationToken)
        {
            Boardgame updatedBoardgame = _boardgameRepository.Update(request.BoardgameId, request.Boardgame);

            return Task.FromResult(updatedBoardgame);
        }
    }
}
