using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Models
{
    internal class IdentityUser
    {
        protected readonly string username;
        protected readonly string password;
        protected readonly byte role;
    }
}
