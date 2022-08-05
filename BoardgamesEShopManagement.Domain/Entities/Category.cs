using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Category : EntityBase
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
