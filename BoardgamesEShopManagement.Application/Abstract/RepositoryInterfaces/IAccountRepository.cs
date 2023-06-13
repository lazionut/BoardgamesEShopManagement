using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        Task Create(Account account);

        Task<List<Account>?> GetAll(int pageIndex, int pageSize);

        Task<int> GetAllCounter();

        Task<Account?> GetById(int id);

        Task<Account?> GetByEmail(string email);

        Task<Account?> GetByAddressId(int addressId);

        void Update(Account account);

        Task<Account?> Delete(int id);

        Task Save();
    }
}