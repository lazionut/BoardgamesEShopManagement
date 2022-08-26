namespace BoardgamesEShopManagement.API.Dto
{
    public class ReviewGetDto
    {
        public int ReviewId { get; set; }
        public string ReviewTitle { get; set; } = null!;
        public string ReviewAuthor { get; set; } = null!;
        public byte ReviewScore { get; set; }
        public string ReviewContent { get; set; } = null!;
        public int ReviewBoardgameId { get; set; }
        public int ReviewAccountId { get; set; }
    }
}
