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
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderRequest, Order>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            Boardgame boardgame = await _unitOfWork.BoardgameRepository.GetById(request.BoardgameId);
            Order order = await _unitOfWork.OrderRepository.GetById(request.OrderId);

            if (boardgame != null && order != null)
            {
                await _unitOfWork.OrderRepository.Create(request.OrderId, request.BoardgameId, order);
                await _unitOfWork.Save();

                return order;
            }
            else
            {
                return null;
            }
        }
    }
}
