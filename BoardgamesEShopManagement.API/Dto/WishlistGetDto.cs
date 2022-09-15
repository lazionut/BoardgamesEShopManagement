namespace BoardgamesEShopManagement.API.Dto
{
    public class WishlistGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int AccountId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public ICollection<object> Boardgames { get; set; } = null!;
    }
}
