namespace BoardgamesEShopManagement.API.Dto
{
    public class WishlistGetDto
    {
        public int WishlistId { get; set; }
        public string WishlistName { get; set; } = null!;
        public int WishlistAccountId { get; set; }
        public DateTime WishlistCreationDate { get; set; }
        public DateTime WishlistUpdateDate { get; set; }
        public ICollection<object> WishlistBoardgames { get; set; } = null!;
    }
}
