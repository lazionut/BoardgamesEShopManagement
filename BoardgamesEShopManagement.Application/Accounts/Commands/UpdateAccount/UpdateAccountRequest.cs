using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountRequest : IRequest<Account>
    {
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}
