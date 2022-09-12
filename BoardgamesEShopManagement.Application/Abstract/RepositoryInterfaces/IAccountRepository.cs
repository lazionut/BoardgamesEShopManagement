using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account?> GetAccountByAddressId(int addressId);
    }
}
