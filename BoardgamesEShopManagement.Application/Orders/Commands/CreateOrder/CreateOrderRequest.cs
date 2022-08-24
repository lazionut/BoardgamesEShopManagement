using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderRequest : IRequest<Order>
    {
        public decimal Total { get; set; }
        public int AccountId { get; set; }
        public int BoardgameId { get; set; }
        public int OrderId { get; set; }   
    }
}
