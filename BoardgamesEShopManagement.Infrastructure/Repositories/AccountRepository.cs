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
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly ShopContext _context;

        public AccountRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Account> ArchiveAccount(int accountId)
        {
            Account searchedAccount = await _context.Accounts
                .SingleOrDefaultAsync(account => account.Id == accountId);

            searchedAccount.FirstName = "Anonymized";
            searchedAccount.LastName = "Anonymized";
            searchedAccount.Email = "Anonymized";
            searchedAccount.Password = "";
            searchedAccount.IsArchived = true;

            return searchedAccount;
        }

    }
}
