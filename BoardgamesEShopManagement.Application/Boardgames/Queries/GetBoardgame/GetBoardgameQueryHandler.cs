using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame
{
    public class GetBoardgameQueryHandler : IRequestHandler<GetBoardgameQuery, Boardgame>
    {
        private readonly IBoardgameRepository _boardgameRepository;

        public GetBoardgameQueryHandler(IBoardgameRepository boardgameRepository)
        {
            _boardgameRepository = boardgameRepository;
        }

        public Task<Boardgame> Handle(GetBoardgameQuery request, CancellationToken cancellationToken)
        {
            Boardgame result = _boardgameRepository.GetById(request.BoardgameId);

            return Task.FromResult(result);
        }
    }
}
