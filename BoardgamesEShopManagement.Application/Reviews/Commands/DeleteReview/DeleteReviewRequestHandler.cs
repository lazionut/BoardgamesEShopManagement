using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewRequestHandler : IRequestHandler<DeleteReviewRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteReviewRequest request, CancellationToken cancellationToken)
        {
            bool isReviewDeleted = await _unitOfWork.ReviewRepository.Delete(request.ReviewId);

            await _unitOfWork.Save();

            return isReviewDeleted;
        }
    }
}
