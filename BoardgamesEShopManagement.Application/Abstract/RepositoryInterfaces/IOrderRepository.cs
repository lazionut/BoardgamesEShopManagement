using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        Task Create(Order order);
        Task CreateItem(int orderId, int boardgameId, Order order);
        Task<List<Order>> GetOrdersListPerAccount(int accountId);
        Task<Order> GetById(int orderId);
        Task<Order> GetByAccount(int accountId, int orderId);
        Task Update(Order order);
        Task<Order> Delete(int orderId);
        Task Save();
    }
}
