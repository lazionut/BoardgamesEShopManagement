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
        [Range(0.1, double.PositiveInfinity)]
        public decimal Total { get; set; }
        public Account Account { get; set; } = null!;
        public int AccountId { get; set; }
        public ICollection<Boardgame> Boardgames { get; set; } = null!;
    }
}
