﻿using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;

namespace BoardgamesEShopManagement.Application.Orders.Queries.GetOrderByAccount
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
            Order? orderByAccount = await _unitOfWork.OrderRepository.GetByAccount(request.OrderAccountId, request.OrderId);

            if (orderByAccount == null)
            {
                return null;
            }

            return orderByAccount;
        }
    }
}