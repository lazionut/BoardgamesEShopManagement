using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame
{
    public class CreateBoardgameRequestHandler : IRequestHandler<CreateBoardgameRequest, Boardgame>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBoardgameRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Boardgame> Handle(CreateBoardgameRequest request, CancellationToken cancellationToken)
        {
            Boardgame boardgame = new Boardgame
            {
                Image = request.BoardgameImage,
                Name = request.BoardgameName,
                Description = request.BoardgameDescription,
                Price = request.BoardgamePrice,
                Link = request.BoardgameLink,
                CategoryId = request.CategoryId,
            };

            await _unitOfWork.BoardgameRepository.Create(boardgame);
            await _unitOfWork.Save();

            return boardgame;
        }
    }
}
