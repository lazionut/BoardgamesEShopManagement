﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.EntityConfigurations;

namespace BoardgamesEShopManagement.Infrastructure
{
    public class ShopContext : DbContext
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Boardgame> Boardgames => Set<Boardgame>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Wishlist> Wishlists => Set<Wishlist>();
        public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BoardgamesEShopDBDev")
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
            .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boardgame>().Property(boardgame => boardgame.BoardgamePrice).HasPrecision(6, 2);
            modelBuilder.Entity<Order>().Property(order => order.Total).HasPrecision(8, 2);
            modelBuilder.Entity<WishlistItem>().HasNoKey();
            modelBuilder.Entity<OrderItem>().HasNoKey();

            modelBuilder.ApplyConfiguration(new BoardgameEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewEntityTypeConfiguration());
        }
    }
}