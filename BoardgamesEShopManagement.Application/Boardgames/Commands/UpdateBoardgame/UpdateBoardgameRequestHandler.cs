using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.UpdateBoardgame
{
    public class UpdateBoardgameRequestHandler : IRequestHandler<UpdateBoardgameRequest, Boardgame?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBoardgameRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Boardgame?> Handle(UpdateBoardgameRequest request, CancellationToken cancellationToken)
        {
            Boardgame? updatedBoardgame = await _unitOfWork.BoardgameRepository.GetById(request.BoardgameId);

            if (updatedBoardgame == null)
            {
                return null;
            }

            Category? searchedCategory = await _unitOfWork.CategoryRepository.GetById(request.BoardgameCategoryId);

            if (searchedCategory == null)
            {
                return null;
            }

            updatedBoardgame.Image = request.BoardgameImage;
            updatedBoardgame.Name = request.BoardgameName;
            updatedBoardgame.ReleaseYear = request.BoardgameReleaseYear;
            updatedBoardgame.Description = request.BoardgameDescription;
            updatedBoardgame.Price = request.BoardgamePrice;
            updatedBoardgame.Link = request.BoardgameLink;
            updatedBoardgame.Quantity = request.BoardgameQuantity;
            updatedBoardgame.CategoryId = request.BoardgameCategoryId;

            updatedBoardgame.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.BoardgameRepository.Update(updatedBoardgame);
            await _unitOfWork.Save();

            return updatedBoardgame;
        }
    }
}