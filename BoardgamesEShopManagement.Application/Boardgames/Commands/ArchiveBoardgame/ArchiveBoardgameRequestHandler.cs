using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveBoardgame
{
    public class ArchiveBoardgameRequestHandler : IRequestHandler<ArchiveBoardgameRequest, Boardgame?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveBoardgameRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Boardgame?> Handle(ArchiveBoardgameRequest request, CancellationToken cancellationToken)
        {
            Boardgame? searchedBoardgame = await _unitOfWork.BoardgameRepository.GetById(request.BoardgameId);

            if (searchedBoardgame == null)
            {
                return null;
            }

            searchedBoardgame.Price = 0;
            searchedBoardgame.Quantity = 0;
            searchedBoardgame.IsArchived = true;

            await _unitOfWork.Save();

            return searchedBoardgame;
        }
    }
}
