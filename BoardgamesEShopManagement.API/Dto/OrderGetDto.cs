using BoardgamesEShopManagement.Domain.Enumerations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class OrderGetDto
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public OrderStatusEnum Status { get; set; }
        public int AccountId { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<OrderBoardgameDto> Boardgames { get; set; } = null!;
    }
}
