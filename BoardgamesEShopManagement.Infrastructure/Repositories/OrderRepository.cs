using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Exceptions;
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

        public async Task Create(int orderId, int boardgameId, Order order)
        {
            Order? searchedOrder = await _context.Orders
                .Include(order => order.Boardgames)
                .SingleOrDefaultAsync(order => order.Id == orderId);
            if (searchedOrder == null)
                throw new GenericItemException($"{searchedOrder} can\'t be found!");

            Boardgame? searchedBoardgame = await _context.Boardgames.SingleOrDefaultAsync(boardgame => boardgame.Id == boardgameId);
            if (searchedBoardgame == null)
                throw new GenericItemException($"{searchedBoardgame} can\'t be found!");

            order.Boardgames.Add(searchedBoardgame);
        }

        public async Task<Order> GetById(int orderId)
        {
            if (orderId >= 0)
            {
                return await _context.Orders
                    .Include(order => order.Boardgames)
                    .SingleOrDefaultAsync(order => order.Id == orderId);
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task<Order> GetByAccount(int accountId, int orderId)
        {
            if (orderId >= 0)
            {
                return await _context.Orders
                    .Include(order => order.Boardgames)
                    .SingleOrDefaultAsync(order => order.AccountId == accountId && order.Id == orderId);
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task<List<Order>> GetOrdersListPerAccount(int accountId)
        {
            if (accountId >= 0)
            {
                return await _context.Orders
                    .Include(order => order.Boardgames)
                    .Where(order => order.AccountId == accountId)
                    .ToListAsync();
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task<bool> Delete(int orderId)
        {
            if (orderId >= 0)
            {
                Order? searchedOrder = await _context.Orders.SingleOrDefaultAsync(order => order.Id == orderId);
                return _context.Orders.Remove(searchedOrder) != null ? true : false;
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
