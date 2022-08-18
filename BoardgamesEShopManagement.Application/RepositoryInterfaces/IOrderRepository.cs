using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        IEnumerable<Order> GetOrders();
        Order GetOrder(int orderId);
        bool DeleteOrder(int orderId);
    }
}
