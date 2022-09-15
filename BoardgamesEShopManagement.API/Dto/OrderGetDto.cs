using BoardgamesEShopManagement.Domain.Enumerations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class OrderGetDto
    {
        public int Id { get; set; }
        public string Total { get; set; } = null!;
        public OrderStatusEnum Status { get; set; }
        public int AccountId { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<object> Boardgames { get; set; } = null!;
    }
}
