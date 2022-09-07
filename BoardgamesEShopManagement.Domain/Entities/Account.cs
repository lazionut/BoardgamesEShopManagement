using System.ComponentModel.DataAnnotations;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Account : EntityBase
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [MaxLength(255)]
        public string Email { get; set; } = null!;

        [MaxLength(128)]
        public string Password { get; set; } = null!;
        public bool IsArchived { get; set; } = false;
        public Address Address { get; set; } = null!;
        public int AddressId { get; set; }
        public ICollection<Review> Review { get; set; } = null!;
        public ICollection<Wishlist> Wishlist { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = null!;
    }
}
