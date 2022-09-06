using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, Order?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            Account? searchedAccount = await _unitOfWork.AccountRepository.GetById(request.OrderAccountId);

            if (searchedAccount == null)
            {
                return null;
            }

            Order order = new Order
            {
                AccountId = request.OrderAccountId,
                Total = 0
            };

            await _unitOfWork.OrderRepository.Create(order);
            await _unitOfWork.Save();

            decimal orderTotalPrice = 0;
            int boardgameIdsCounter = request.OrderBoardgameIds.Count;
            for (int index = 0; index < boardgameIdsCounter; ++index)
            {
                Boardgame? boardgame = await _unitOfWork
                    .BoardgameRepository
                    .GetById(request.OrderBoardgameIds[index]);

                if (boardgame == null)
                {
                    return null;
                }

                boardgame.Quantity -= request.OrderBoardgameQuantities[index];

                if (boardgame.Quantity < 0)
                {
                    return null;
                }

                await _unitOfWork.BoardgameRepository.Update(boardgame);

                orderTotalPrice += ( boardgame.Price * request.OrderBoardgameQuantities[index]);

                await _unitOfWork.OrderRepository
                    .CreateItem(order.Id, request.OrderBoardgameIds[index], order);
            }

            order.Total = orderTotalPrice;

            await _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.Save();

            return order;
        }
    }
}
