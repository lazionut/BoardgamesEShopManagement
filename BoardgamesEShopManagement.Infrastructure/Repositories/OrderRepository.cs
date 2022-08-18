using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Exceptions;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public readonly List<Order> orders = new();

        public void CreateOrder(Order order)
        {
            orders.Add(order);
            order.Id = orders.Count;
        }

        public IEnumerable<Order> GetOrders()
        {
            return orders;
        }

        public Order GetOrder(int orderId)
        {
            if (orderId >= 0)
            {
                return orders.FirstOrDefault(order => order.Id == orderId);
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public bool DeleteOrder(int orderId)
        {
            if (orderId >= 0)
            {
                Order searchedOrder = orders.FirstOrDefault(order => order.Id == orderId);
                return orders.Remove(searchedOrder);
            }
            else
            {
                throw new NegativeIdException();
            }
        }
    }
}
