namespace BoardgamesEShopManagement.API.Dto
{
    public class BoardgameGetDto
    {
        public int BoardgameId { get; set; }
        public string? BoardgameImage { get; set; }
        public string BoardgameName { get; set; } = null!;
        public string? BoardgameDescription { get; set; }
        public decimal BoardgamePrice { get; set; }
        public string? BoardgameLink { get; set; }
        public int BoardgameCategoryId { get; set; }
    }
}
