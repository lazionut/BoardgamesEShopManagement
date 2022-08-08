using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList
{
    public class GetBoardgamesListQueryHandler : IRequestHandler<GetBoardgamesListQuery, IEnumerable<BoardgamesListVm>>
    {
        private readonly IBoardgameRepository _boardgameRepository;

        public GetBoardgamesListQueryHandler(IBoardgameRepository boardgameRepository)
        {
            _boardgameRepository = boardgameRepository;
        }

        public Task<IEnumerable<BoardgamesListVm>> Handle(GetBoardgamesListQuery request, CancellationToken cancellationToken)
        {
            var result = _boardgameRepository.GetAll().Select(boardgame => new BoardgamesListVm
            {
                Id = boardgame.Id,
                Image = boardgame.BoardgameImage,
                Name = boardgame.BoardgameName,
                Description = boardgame.BoardgameDescription,
                Price = boardgame.BoardgamePrice
            });

            return Task.FromResult(result);
        }
    }
}
