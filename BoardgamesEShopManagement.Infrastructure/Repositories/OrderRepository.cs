using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopContext _context;

        public OrderRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task Create(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task CreateItem(int orderId, int boardgameId, Order order)
        {
            Order searchedOrder = await _context.Orders
                .Include(order => order.Boardgames)
                .SingleOrDefaultAsync(order => order.Id == orderId);

            Boardgame searchedBoardgame = await _context.Boardgames
                .SingleOrDefaultAsync(boardgame => boardgame.Id == boardgameId);

            order.Boardgames.Add(searchedBoardgame);
        }

        public async Task<Order> GetById(int orderId)
        {
            return await _context.Orders
                .Include(order => order.Boardgames)
                .SingleOrDefaultAsync(order => order.Id == orderId);
        }

        public async Task<Order> GetByAccount(int accountId, int orderId)
        {
            return await _context.Orders
                .Include(order => order.Boardgames)
                .SingleOrDefaultAsync(order => order.AccountId == accountId && order.Id == orderId);
        }

        public async Task<List<Order>> GetOrdersListPerAccount(int accountId)
        {
            return await _context.Orders
                .Include(order => order.Boardgames)
                .Where(order => order.AccountId == accountId)
                .ToListAsync();
        }
        public async Task Update(Order order)
        {
            _context.Update(order);
        }

        public async Task<Order> Delete(int orderId)
        {
            Order searchedOrder = await _context.Orders
                .SingleOrDefaultAsync(order => order.Id == orderId);

            if (searchedOrder == null)
            {
                return null;
            }

            _context.Orders.Remove(searchedOrder);

            return searchedOrder;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
