using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;
using Microsoft.EntityFrameworkCore;

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
            Boardgame updatedBoardgame = await _unitOfWork.BoardgameRepository.GetById(request.BoardgameId);

            if (updatedBoardgame == null)
            {
                return null;
            }

            updatedBoardgame.Image = request.Image ?? updatedBoardgame.Image;
            updatedBoardgame.Name = request.Name ?? updatedBoardgame.Name;
            updatedBoardgame.Description = request.Description ?? updatedBoardgame.Description;
            if (updatedBoardgame.Price != request.Price)
            {
                updatedBoardgame.Price = request.Price;
            }
            updatedBoardgame.Link = request.Link ?? updatedBoardgame.Link;
            updatedBoardgame.CategoryId = request.CategoryId;

            await _unitOfWork.BoardgameRepository.Update(updatedBoardgame);

            await _unitOfWork.Save();

            return updatedBoardgame;
        }
    }
}
