using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Models
{
    internal class Account : IdentityUser
    {
        protected int PersonId { get; set; }
        protected int AccountId { get; set; }
        protected DateTime CreationDate { get; set; }
    }
}
