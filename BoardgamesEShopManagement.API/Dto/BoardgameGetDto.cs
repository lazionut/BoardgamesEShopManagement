namespace BoardgamesEShopManagement.API.Dto
{
    public class BoardgameGetDto
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Link { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
    }
}
