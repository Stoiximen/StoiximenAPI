using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stoiximen.Domain.Models;
using Stoiximen.Infrastructure.Constants;

namespace Stoiximen.Infrastructure.EF.Context
{
    public class StoiximenDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StoiximenDbContext(DbContextOptions<StoiximenDbContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment == "Development")
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            }

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // Entities are not tracked so changes wont happen automatically

            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditFields()
        {
            var currentUser = GetCurrentUser();
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditableEntity auditableEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditableEntity.CreatedAt = now;
                            auditableEntity.CreatedBy = currentUser;
                            auditableEntity.UpdatedAt = now;
                            auditableEntity.UpdatedBy = currentUser;
                            break;

                        case EntityState.Modified:
                            auditableEntity.UpdatedAt = now;
                            auditableEntity.UpdatedBy = currentUser;

                            // Check if this is a soft delete
                            if (entry.Entity is SoftDeletableEntity softDeletableEntity)
                            {
                                var deletedAtProperty = entry.Property(nameof(SoftDeletableEntity.DeletedAt));

                                // Only set DeletedBy if DeletedAt was modified and has a value
                                if (deletedAtProperty.IsModified && softDeletableEntity.DeletedAt.HasValue)
                                {
                                    softDeletableEntity.DeletedBy = currentUser;
                                }
                            }
                            break;
                    }
                }
            }
        }

        private string GetCurrentUser()
        {
            var httpContext = _httpContextAccessor?.HttpContext;

            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                var telegramId = httpContext.User.FindFirst(Claims.TelegramUserId)?.Value;
                var firstName = httpContext.User.FindFirst(Claims.TelegramFirstName)?.Value;
                var lastName = httpContext.User.FindFirst(Claims.TelegramLastName)?.Value;

                if (!string.IsNullOrEmpty(telegramId))
                {
                    var fullName = $"{firstName} {lastName}".Trim();
                    return string.IsNullOrEmpty(fullName) ? telegramId : $"{telegramId} ({fullName})";
                }
            }

            return "SYSTEM";
        }

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
            var staticDate = new DateTime(2025, 6, 21, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Subscription>().HasData(
             new Subscription
             {
                 Id = 1,
                 Name = "Basic Plan",
                 Description = "Basic access to the platform.",
                 Price = 9.99m,
                 CreatedAt = staticDate,
                 CreatedBy = "SYSTEM",
                 UpdatedAt = staticDate,
                 UpdatedBy = "SYSTEM"
             },
             new Subscription
             {
                 Id = 2,
                 Name = "Premium Plan",
                 Description = "Full access to all features.",
                 Price = 29.99m,
                 CreatedAt = staticDate,
                 CreatedBy = "SYSTEM",
                 UpdatedAt = staticDate,
                 UpdatedBy = "SYSTEM"
             }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "1111 Vale to telegramId sou edw",
                    Name = "Tony rs",
                    SubscriptionStatus = 0,
                    CreatedAt = staticDate,
                    CreatedBy = "SYSTEM",
                    UpdatedAt = staticDate,
                    UpdatedBy = "SYSTEM"
                },
                new User
                {
                    Id = "2222 Vale to telegramId sou edw",
                    Name = "Xaris tsel",
                    SubscriptionStatus = 0,
                    CreatedAt = staticDate,
                    CreatedBy = "SYSTEM",
                    UpdatedAt = staticDate,
                    UpdatedBy = "SYSTEM"
                },
                new User
                {
                    Id = "8011353457",
                    Name = "GN",
                    SubscriptionStatus = 0,
                    CreatedAt = staticDate,
                    CreatedBy = "SYSTEM",
                    UpdatedAt = staticDate,
                    UpdatedBy = "SYSTEM"
                }
            );
        }
    }
}