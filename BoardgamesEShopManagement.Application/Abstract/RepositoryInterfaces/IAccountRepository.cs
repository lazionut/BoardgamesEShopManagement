using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> UpdateAccount(int accountId, Account account);
    }
}
