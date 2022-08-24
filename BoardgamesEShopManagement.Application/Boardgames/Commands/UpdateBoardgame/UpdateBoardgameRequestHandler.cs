using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.UpdateBoardgame
{
    public class UpdateBoardgameRequestHandler : IRequestHandler<UpdateBoardgameRequest, Boardgame>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBoardgameRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Boardgame> Handle(UpdateBoardgameRequest request, CancellationToken cancellationToken)
        {
            Boardgame updatedBoardgame = await _unitOfWork.BoardgameRepository.UpdateBoardgame(request.BoardgameId, request.Boardgame);

            await _unitOfWork.Save();

            return updatedBoardgame;
        }
    }
}
