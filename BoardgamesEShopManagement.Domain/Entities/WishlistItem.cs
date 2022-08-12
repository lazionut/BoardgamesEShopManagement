using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class WishlistItem : EntityBase
    {
        public Boardgame Boardgame { get; set; }
        public int Quantity { get; set; }
    }
}
