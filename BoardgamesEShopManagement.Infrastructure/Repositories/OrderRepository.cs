using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<Order> _logger;

        public OrderRepository(ShopContext context, ILogger<Order> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(Order order)
        {
            _logger.LogInformation("Preparing to add order to the database...");
            await _context.Orders.AddAsync(order);
        }

        public async Task AddItems(Order order, List<OrderItem> orderItems)
        {
            _logger.LogInformation("Preparing to add the boardgame in the order...");
            order.OrderItems = orderItems;

            _logger.LogInformation("Preparing to update the order...");
            _context.Update(order);
        }

        public async Task<Order?> GetById(int orderId)
        {
            _logger.LogInformation("Trying to get the order by it's identifier...");
            return await _context.Orders
                .Include(order => order.OrderItems)
                .ThenInclude(order => order.Boardgame)
                .SingleOrDefaultAsync(order => order.Id == orderId);
        }

        public async Task<Order?> GetByAccount(int accountId, int orderId)
        {
            _logger.LogInformation("Trying to get the order by an account and it's identifier...");
            return await _context.Orders
                .Include(order => order.OrderItems)
                .SingleOrDefaultAsync(order => order.AccountId == accountId && order.Id == orderId);
        }

        public async Task<List<Order>> GetOrdersListPerAccount(int accountId, int pageIndex, int pageSize)
        {
            _logger.LogInformation("Getting the list of orders by an account identifier...");
            return await _context.Orders
                .Include(order => order.OrderItems)
                .Where(order => order.AccountId == accountId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task Update(Order order)
        {
            _logger.LogInformation("Preparing to update order from the database...");
            _context.Update(order);
        }

        public async Task<Order?> Delete(int orderId)
        {
            _logger.LogInformation("Trying to get the order by it's identifier...");
            Order? searchedOrder = await _context.Orders
                .SingleOrDefaultAsync(order => order.Id == orderId);

            _logger.LogError("Could not find the order.");
            if (searchedOrder == null)
            {
                return null;
            }

            _logger.LogInformation("Preparing to remove order from the database...");
            _context.Orders.Remove(searchedOrder);

            return searchedOrder;
        }

        public async Task Save()
        {
            _logger.LogInformation("Saving current changes to the database...");
            await _context.SaveChangesAsync();
        }
    }
}
