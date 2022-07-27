using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Models
{
    internal class Account : IdentityUser
    {
        protected readonly int personId;
        protected readonly int accountId;
        protected readonly DateTime creationDate;
    }
}
