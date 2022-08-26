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
        public string AccountFirstName { get; set; } = null!;
        public string AccountLastName { get; set; } = null!;
        public string AccountEmail { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
    }
}
