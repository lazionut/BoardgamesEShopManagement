using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.LoginAccount
{
    public class LoginAccountQuery : IRequest<string>
    {
        public string AccountEmail { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
    }
}
