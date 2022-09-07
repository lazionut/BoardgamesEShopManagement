using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame
{
    public class CreateBoardgameRequestHandler : IRequestHandler<CreateBoardgameRequest, Boardgame?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBoardgameRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Boardgame?> Handle(CreateBoardgameRequest request, CancellationToken cancellationToken)
        {
            Category? searchedCategory = await _unitOfWork.CategoryRepository.GetById(request.BoardgameCategoryId);

            if (searchedCategory == null)
            {
                return null;
            }

            Boardgame boardgame = new Boardgame
            {
                Image = request.BoardgameImage,
                Name = request.BoardgameName,
                ReleaseYear = request.BoardgameReleaseYear,
                Description = request.BoardgameDescription,
                Price = request.BoardgamePrice,
                Link = request.BoardgameLink,
                Quantity = request.BoardgameQuantity,
                CategoryId = request.BoardgameCategoryId,
                IsArchived = false
            };

            await _unitOfWork.BoardgameRepository.Create(boardgame);
            await _unitOfWork.Save();

            return boardgame;
        }
    }
}
