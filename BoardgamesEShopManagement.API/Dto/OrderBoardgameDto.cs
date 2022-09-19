using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Dto
{
    public class OrderBoardgameDto
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
