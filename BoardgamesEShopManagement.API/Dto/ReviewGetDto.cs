namespace BoardgamesEShopManagement.API.Dto
{
    public class ReviewGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public byte Score { get; set; }
        public string Content { get; set; } = null!;
        public int BoardgameId { get; set; }
        public int AccountId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
