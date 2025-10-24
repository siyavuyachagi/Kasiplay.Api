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

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Cup> Cups { get; set; }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<Federation> Federations { get; set; }
        public DbSet<FileResource> FileResources { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<MatchRecord> MatchRecords { get; set; }
        public DbSet<Official> Officials { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<PhysicalAddress> PhysicalAddresss { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerInjury> PlayerInjuries { get; set; }
        public DbSet<PlayerTransferRecord> PlayerTransferRecords { get; set; }
        public DbSet<President> Presidents { get; set; }
        public DbSet<RegistrationApplication> RegistrationApplications { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantSubscription> TenantSubscriptions { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }



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


            // One Tenant -> Many TenantSubscriptions (history)
            builder.Entity<TenantSubscription>()
                .HasOne(ts => ts.Tenant)
                .WithMany() // Tenant can have many past subscriptions
                .HasForeignKey(ts => ts.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // One Tenant -> One CurrentSubscription (active link)
            builder.Entity<Tenant>()
                .HasOne(t => t.TenantSubscription)
                .WithOne() // no back-navigation to TenantSubscription
                .HasForeignKey<Tenant>(t => t.TenantSubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}