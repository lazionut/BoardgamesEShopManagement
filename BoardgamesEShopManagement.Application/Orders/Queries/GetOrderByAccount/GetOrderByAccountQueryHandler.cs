using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetOrderByAccount
{
    public class GetOrderByAccountQueryHandler : IRequestHandler<GetOrderByAccountQuery, Order?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByAccountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> Handle(GetOrderByAccountQuery request, CancellationToken cancellationToken)
        {
            Order? orderByAccount = await _unitOfWork.OrderRepository.GetByAccount(request.AccountId, request.OrderId);

            if (orderByAccount == null)
            {
                return null;
            }

            return orderByAccount;
        }
    }
}
