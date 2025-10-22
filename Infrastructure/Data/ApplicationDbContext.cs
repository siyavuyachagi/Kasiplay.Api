using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // <-- Use this namespace
using Microsoft.EntityFrameworkCore;
using PhysicalAddress = Domain.Entities.PhysicalAddress;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> // <-- Use the correct base class
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
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

                // Configure roles relationship
                entity.HasMany(e => e.IdentityUserRoles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                entity.HasMany(e => e.IdentityRoles)
                    .WithMany()
                    .UsingEntity(j => j.ToTable("AspNetUserRoles"));
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