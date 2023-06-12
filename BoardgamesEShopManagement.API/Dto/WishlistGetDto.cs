namespace BoardgamesEShopManagement.API.Dto
{
    public class WishlistGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public ICollection<WishlistBoardgameDto> Boardgames { get; set; } = null!;
    }
}