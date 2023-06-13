using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

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

            bool isBoardgameReviewedByAccount = await _unitOfWork.ReviewRepository.IsBoardgameReviewed(request.ReviewAccountId, request.ReviewBoardgameId);

            if (isBoardgameReviewedByAccount == true)
            {
                return null;
            }

            Review review = new Review
            {
                Title = request.ReviewTitle,
                Author = GetAuthorName(searchedAccount.FirstName, searchedAccount.LastName),
                Score = request.ReviewScore,
                Content = request.ReviewContent,
                BoardgameId = request.ReviewBoardgameId,
                AccountId = request.ReviewAccountId,
            };

            await _unitOfWork.ReviewRepository.Create(review);
            await _unitOfWork.Save();

            return review;
        }

        private string GetAuthorName(string firstName, string lastName) => firstName + " " + lastName.Substring(0, 1) + ".";
    }
}