using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Dto
{
    public class OrderGetDto
    {
        public int OrderId { get; set; }
        public string OrderTotal { get; set; } = null!;
        public string OrderAccountId { get; set; } = null!;
        public ICollection<object> OrderBoardgames { get; set; } = null!;
    }
}
