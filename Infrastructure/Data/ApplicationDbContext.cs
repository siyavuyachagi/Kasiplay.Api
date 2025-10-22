using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Add parameterless constructor for EF Core migrations
        public ApplicationDbContext() : base()
        {
        }

        public DbSet<PhysicalAddress> PhysicalAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure ApplicationUser
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Gender)
                    .HasMaxLength(20);

                entity.Property(e => e.Bio)
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.IsDeleted)
                    .HasDefaultValue(false);

                // Configure PhysicalAddress relationship
                entity.HasOne(e => e.PhysicalAddress)
                    .WithMany()
                    .HasForeignKey(e => e.PhysicalAddressId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure PhysicalAddress
            builder.Entity<PhysicalAddress>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");
            });
        }


    }
}