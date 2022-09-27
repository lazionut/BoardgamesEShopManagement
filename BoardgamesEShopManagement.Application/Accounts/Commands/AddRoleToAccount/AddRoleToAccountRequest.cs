using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.AddRoleToAccount
{
    public class AddRoleToAccountRequest : IRequest<bool>
    {
        public string Email { get; set; } = null!;
        public string RoleName { get; set; } = null!;
    }
}
