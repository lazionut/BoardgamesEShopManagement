using BoardgamesEShopManagement.Domain.Enumerations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class OrderGetDto
    {
        public int OrderId { get; set; }
        public string OrderTotal { get; set; } = null!;
        public OrderStatusEnum OrderStatus { get; set; }
        public int OrderAccountId { get; set; }
        public ICollection<object> OrderBoardgames { get; set; } = null!;
    }
}
