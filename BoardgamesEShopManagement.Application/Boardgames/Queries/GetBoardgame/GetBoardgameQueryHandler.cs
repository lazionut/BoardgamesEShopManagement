using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame
{
    public class GetBoardgameQueryHandler : IRequestHandler<GetBoardgameQuery, Boardgame>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Boardgame> Handle(GetBoardgameQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetById(request.BoardgameId);
        }
    }
}
