using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoardgamesEShopManagement.Infrastructure
{
    public class ShopContext : IdentityDbContext<Account, IdentityRole<int>, int>
    {
        public ShopContext()
        {
        }

        public ShopContext(DbContextOptions<ShopContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Boardgame> Boardgames => Set<Boardgame>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Wishlist> Wishlists => Set<Wishlist>();
        public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            builder.ApplyConfiguration(new BoardgameEntityTypeConfiguration());
            builder.ApplyConfiguration(new AddressEntityTypeConfiguration());
            builder.ApplyConfiguration(new AccountEntityTypeConfiguration());
            builder.ApplyConfiguration(new ReviewEntityTypeConfiguration());
            builder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            builder.ApplyConfiguration(new WishlistEntityTypeConfiguration());
        }
    }
}