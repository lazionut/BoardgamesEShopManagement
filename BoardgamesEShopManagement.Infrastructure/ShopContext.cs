using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.EntityConfigurations;

namespace BoardgamesEShopManagement.Infrastructure
{
    public class ShopContext : DbContext
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder
                .UseSqlServer(configuration.GetConnectionString("Default"))
                //.UseSqlServer(configuration.GetConnectionString("Test"))
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BoardgameEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AddressEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WishlistEntityTypeConfiguration());
        }
    }
}
