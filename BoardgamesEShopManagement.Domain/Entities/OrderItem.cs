using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int BoardgameId { get; set; }
        public Boardgame Boardgame { get; set; } = null!;

        [Precision(12, 2)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}