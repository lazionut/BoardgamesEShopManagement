using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        Task Create(Order order);
        Task AddItems(Order order, List<OrderItem> orderItems);
        Task<List<Order>> GetPerAccount(int accountId, int pageIndex, int pageSize);
        Task<int> GetPerAccountCounter(int accountId);
        Task<Order?> GetById(int orderId);
        Task<Order?> GetByAccount(int accountId, int orderId);
        Task Update(Order order);
        Task<Order?> Delete(int orderId);
        Task Save();
    }
}
