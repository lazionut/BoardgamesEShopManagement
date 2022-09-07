namespace BoardgamesEShopManagement.Domain.Entities
{
    public class WishlistItem
    {
        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; } = null!;
        public int BoardgameId { get; set; }
        public Boardgame Boardgame { get; set; } = null!;
    }
}
