using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Models
{
    internal class IdentityUser
    {
        protected string Username { get; set; }
        protected string Password { get; set; }
        protected string Role { get; set; }
    }
}
