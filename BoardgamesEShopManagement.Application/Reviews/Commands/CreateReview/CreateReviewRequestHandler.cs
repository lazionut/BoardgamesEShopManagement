using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewRequestHandler : IRequestHandler<CreateReviewRequest, Review?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateReviewRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review?> Handle(CreateReviewRequest request, CancellationToken cancellationToken)
        {
            Account? searchedAccount = await _unitOfWork.AccountRepository.GetById(request.ReviewAccountId);

            if (searchedAccount == null)
            {
                return null;
            }

            Boardgame? searchedBoardgame = await _unitOfWork.BoardgameRepository.GetById(request.ReviewBoardgameId);

            if (searchedBoardgame == null)
            {
                return null;
            }

            Review? searchedReviewByAccount = await _unitOfWork.ReviewRepository.GetByAccountId(request.ReviewAccountId);
            Review? searchedReviewByBoardgame = await _unitOfWork.ReviewRepository.GetByBoardgameId(request.ReviewBoardgameId);

            if (searchedReviewByAccount != null && searchedReviewByBoardgame != null)
            {
                return null;
            }

            Review review = new Review
            {
                Title = request.ReviewTitle,
                Author = searchedAccount.FirstName + ' ' + searchedAccount.LastName,
                Score = request.ReviewScore,
                Content = request.ReviewContent,
                BoardgameId = request.ReviewBoardgameId,
                AccountId = request.ReviewAccountId,
            };

            await _unitOfWork.ReviewRepository.Create(review);
            await _unitOfWork.Save();

            return review;
        }
    }
}
