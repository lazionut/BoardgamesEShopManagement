using BoardgamesEShopManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.API.Dto
{
    public class BoardgamePostPutDto
    {
        public string? BoardgameImage { get; set; }

        [MaxLength(50)]
        public string BoardgameName { get; set; } = null!;

        [MaxLength(4000)]
        public string? BoardgameDescription { get; set; }
        public decimal BoardgamePrice { get; set; }

        [MaxLength(2000)]
        public string? BoardgameLink { get; set; }
        public int BoardgameCategoryId { get; set; }
    }
}
