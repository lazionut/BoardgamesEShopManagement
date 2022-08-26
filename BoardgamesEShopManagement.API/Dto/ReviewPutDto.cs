using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class ReviewPutDto
    {
        [MaxLength(50)]
        public string ReviewTitle { get; set; } = null!;

        [MaxLength(4000)]
        public string ReviewContent { get; set; } = null!;
    }
}
