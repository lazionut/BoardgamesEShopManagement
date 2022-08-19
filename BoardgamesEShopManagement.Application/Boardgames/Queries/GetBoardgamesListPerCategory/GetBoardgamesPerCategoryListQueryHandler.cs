using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategory
{
    public class GetBoardgamesPerCategoryListQueryHandler : IRequestHandler<GetBoardgamesPerCategoryListQuery, IEnumerable<BoardgamesPerCategoryListVm>>
    {
        private readonly IBoardgameRepository _boardgameRepository;

        public GetBoardgamesPerCategoryListQueryHandler(IBoardgameRepository boardgameRepository)
        {
            _boardgameRepository = boardgameRepository;
        }

        public Task<IEnumerable<BoardgamesPerCategoryListVm>> Handle(GetBoardgamesPerCategoryListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<BoardgamesPerCategoryListVm> result = _boardgameRepository.GetBoardgamesPerCategory(request.CategoryId).Select(boardgame => new BoardgamesPerCategoryListVm
            {
                Id = boardgame.Id,
                BoardgameImage = boardgame.Image,
                BoardgameName = boardgame.Name,
                BoardgameDescription = boardgame.Description,
                BoardgamePrice = boardgame.Price,
                BoardgameLink = boardgame.Link
            });

            return Task.FromResult(result);
        }
    }
}
