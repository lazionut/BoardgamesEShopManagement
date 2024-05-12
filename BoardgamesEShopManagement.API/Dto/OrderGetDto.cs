using BoardgamesEShopManagement.Domain.Enumerations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class OrderGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Total { get; set; }
        public OrderStatusMode Status { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<OrderBoardgameDto> Boardgames { get; set; } = null!;
    }
}