using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(ShopContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(Order order)
        {
            _logger.LogInformation("Creating {Order}", typeof(Order).Name);
            await _context.Orders.AddAsync(order);
        }

        public void AddItems(Order order, List<OrderItem> orderItems)
        {
            order.OrderItems = orderItems;

            _logger.LogInformation("Update {OrderItem} for order {Order}", typeof(OrderItem).Name, typeof(Order).Name);
            _context.Update(order);
        }

        public async Task<Order?> GetById(int orderId)
        {
            _logger.LogInformation("Reading {Order} with id {Id}", typeof(Order).Name, orderId);
            return await _context.Orders
                .Include(order => order.OrderItems)
                .ThenInclude(order => order.Boardgame)
                .SingleOrDefaultAsync(order => order.Id == orderId);
        }

        public async Task<Order?> GetByAccount(int accountId, int orderId)
        {
            _logger.LogInformation("Reading {Order} with account id {AccountId}", typeof(Order).Name, accountId);
            return await _context.Orders
                .Include(order => order.OrderItems)
                .ThenInclude(order => order.Boardgame)
                .SingleOrDefaultAsync(order => order.AccountId == accountId && order.Id == orderId);
        }

        public async Task<List<Order>> GetAll(int pageIndex, int pageSize)
        {
            _logger.LogInformation("Reading all {Order}", typeof(Order).Name);
            return await _context.Orders
                .Include(order => order.OrderItems)
                .ThenInclude(order => order.Boardgame)
                .OrderByDescending(order => order.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetAllCounter()
        {
            _logger.LogInformation("Reading number of all {Order}", typeof(Order).Name);
            return await _context.Orders.CountAsync();
        }

        public async Task<List<Order>> GetPerAccount(int accountId, int pageIndex, int pageSize)
        {
            _logger.LogInformation("Reading all {Order} with account id {AccountId}", typeof(Order).Name, accountId);
            return await _context.Orders
                .Include(order => order.OrderItems)
                .ThenInclude(order => order.Boardgame)
                .OrderByDescending(order => order.Id)
                .Where(order => order.AccountId == accountId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetPerAccountCounter(int accountId)
        {
            _logger.LogInformation("Reading number of all {Order} with account id {AccountId}", typeof(Order).Name, accountId);
            return await _context.Orders
                .Include(order => order.OrderItems)
                .ThenInclude(order => order.Boardgame)
                .Where(order => order.AccountId == accountId).CountAsync();
        }

        public void Update(Order order)
        {
            _logger.LogInformation("Updating {Order} with id {Id}", typeof(Order).Name, order.Id);
            _context.Update(order);
        }

        public async Task<Order?> Delete(int orderId)
        {
            _logger.LogInformation("Reading {Order} with id {Id}", typeof(Order).Name, orderId);
            Order? searchedOrder = await _context.Orders
                .SingleOrDefaultAsync(order => order.Id == orderId);

            if (searchedOrder == null)
            {
                return null;
            }

            _logger.LogInformation("Removing {Order} with id {Id}", typeof(Order).Name, orderId);
            _context.Orders.Remove(searchedOrder);

            return searchedOrder;
        }
    }
}