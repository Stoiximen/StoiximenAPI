using Microsoft.EntityFrameworkCore;
using Stoiximen.Domain.Models;

namespace Stoiximen.Infrastructure.EF.Context
{
    public class StoiximenDbContext : DbContext
    {
        public StoiximenDbContext(DbContextOptions<StoiximenDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SubscriptionStatus)
                    .IsRequired();
            });

            // Configure Subscription entity
            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasPrecision(18, 2);
            });

            SeedMockData(modelBuilder);
        }
        private void SeedMockData(ModelBuilder modelBuilder)
        {
            // 🔹 Static Seed Data
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription
                {
                    Id = 1,
                    Name = "Basic Plan",
                    Description = "Basic access to the platform.",
                    Price = 9.99m
                },
                new Subscription
                {
                    Id = 2,
                    Name = "Premium Plan",
                    Description = "Full access to all features.",
                    Price = 29.99m
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "1111 Vale to telegramId sou edw",
                    Name = "Tony rs",
                    SubscriptionStatus = 0,
                },
                new User
                {
                    Id = "2222 Vale to telegramId sou edw",
                    Name = "Xaris tsel",
                    SubscriptionStatus = 0
                },
                new User
                {
                    Id = "23142353",
                    Name = "GN",
                    SubscriptionStatus = 0
                }
            );
        }
    }
}