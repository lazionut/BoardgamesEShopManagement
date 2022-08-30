using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountRequest : IRequest<Account>
    {
        public string AccountFirstName { get; set; } = null!;
        public string AccountLastName { get; set; } = null!;
        public string AccountEmail { get; set; } = null!;
        public string AccountUsername { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
        public int AccountAddressId { get; set; } 
    }
}
