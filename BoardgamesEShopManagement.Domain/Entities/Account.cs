using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Account : IdentityUser<int>
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        public bool IsArchived { get; set; } = false;
        public Address Address { get; set; } = null!;
        public int AddressId { get; set; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Review> Review { get; set; } = null!;
        public ICollection<Wishlist> Wishlist { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = null!;
    }
}