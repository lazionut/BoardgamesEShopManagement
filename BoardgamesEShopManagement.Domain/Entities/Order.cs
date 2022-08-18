using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Order : EntityBase
    {
        [MaxLength(100)]
        public string BuyerName { get; set; } = null!;
        public decimal Total { get; set; }
        public ICollection<Boardgame> Boardgames { get; set; } = null!;
    }
}
