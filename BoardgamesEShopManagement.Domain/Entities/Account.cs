using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Account : EntityBase
    {
        public int AccountId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
