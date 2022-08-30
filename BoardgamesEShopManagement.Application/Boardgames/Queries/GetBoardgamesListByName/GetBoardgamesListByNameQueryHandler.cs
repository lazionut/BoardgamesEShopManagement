using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByName
{
    public class GetBoardgamesListByNameQueryHandler : IRequestHandler<GetBoardgamesListByNameQuery, List<Boardgame>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgamesListByNameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Boardgame>> Handle(GetBoardgamesListByNameQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetBoardgamesByName(request.BoardgameNameCharacters);
        }
    }
}
