namespace BoardgamesEShopManagement.Domain.Entities
{
    public class OrderItem
    { 
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int BoardgameId { get; set; }
        public Boardgame Boardgame { get; set; } = null!;
    }
}
