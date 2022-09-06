using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetOrdersListPerAccount
{
    public class GetOrdersListPerAccountQueryHandler : IRequestHandler<GetOrdersListPerAccountQuery, List<Order>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersListPerAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Order>?> Handle(GetOrdersListPerAccountQuery request, CancellationToken cancellationToken)
        {
            Order? searchedOrder = await _unitOfWork.OrderRepository.GetById(request.OrderAccountId);

            if (searchedOrder == null)
            {
                return null;
            }

            return await _unitOfWork.OrderRepository.GetOrdersListPerAccount
                (request.OrderAccountId, request.OrderPageIndex, request.OrderPageSize);
        }
    }
}
