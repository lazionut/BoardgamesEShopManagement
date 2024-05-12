using BoardgamesEShopManagement.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Order : EntityBase
    {
        [Range(0.1, double.PositiveInfinity)]
        public decimal Total { get; set; }

        public OrderStatusMode Status { get; set; } = OrderStatusMode.Created;
        public Account Account { get; set; } = null!;
        public int AccountId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = null!;

        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        [MaxLength(600)]
        public string Address { get; set; } = null!;
    }
}