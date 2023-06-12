using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Address : EntityBase
    {
        [MaxLength(200)]
        public string Details { get; set; } = null!;

        [MaxLength(50)]
        public string City { get; set; } = null!;

        [MaxLength(50)]
        public string County { get; set; } = null!;

        [MaxLength(150)]
        public string Country { get; set; } = null!;

        [MaxLength(30)]
        public string Phone { get; set; } = null!;

        public bool IsArchived { get; set; } = false;

        [JsonIgnore]
        public Account Account { get; set; } = null!;
    }
}