using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Exceptions;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly ShopContext _context;

        public AccountRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Account> UpdateAccount(int accountId, Account account)
        {
            if (accountId >= 0)
            {
                Account searchedAccount = await _context.Accounts.SingleOrDefaultAsync(account => account.Id == accountId);
                searchedAccount.FirstName = account.FirstName ?? searchedAccount.FirstName;
                searchedAccount.LastName = account.LastName ?? searchedAccount.LastName;
                searchedAccount.Email = account.Email ?? searchedAccount.Email;
                searchedAccount.Password = account.Password ?? searchedAccount.Password;

                _context.Update(searchedAccount);

                return searchedAccount;
            }
            else
            {
                throw new NegativeIdException();
            }
        }
    }
}
