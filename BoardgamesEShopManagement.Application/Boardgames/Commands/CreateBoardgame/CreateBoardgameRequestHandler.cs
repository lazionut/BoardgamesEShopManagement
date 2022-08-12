using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame
{
    public class CreateBoardgameRequestHandler : IRequestHandler<CreateBoardgameRequest, int>
    {
        private readonly IBoardgameRepository _boardgameRepository;

        public CreateBoardgameRequestHandler(IBoardgameRepository boardgameRepository)
        {
            _boardgameRepository = boardgameRepository;
        }

        public Task<int> Handle(CreateBoardgameRequest request, CancellationToken cancellationToken)
        {
            Boardgame boardgame = new Boardgame
            {
                CategoryId = request.CategoryId,
                BoardgameImage = request.BoardgameImage,
                BoardgameName = request.BoardgameName,
                BoardgameDescription = request.BoardgameDescription,
                BoardgamePrice = request.BoardgamePrice,
                BoardgameLink = request.BoardgameLink
            };
            _boardgameRepository.Create(boardgame);

            return Task.FromResult(boardgame.Id);
        }
    }
}
