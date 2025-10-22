using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string LastName { get; set; } = new string("");
        public string FirstName { get; set; } = new string("");
        public string Gender { get; set; }
        public string Bio { get; set; } = new string("");
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? Avatar { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual string? Discriminator { get; set; }

        //public Guid? OrganizationId { get; set; }
        //[ForeignKey(nameof(OrganizationId))]
        //public virtual Organization? Organization { get; set; }

        public Guid? PhysicalAddressId { get; set; }
        [ForeignKey(nameof(PhysicalAddressId))]
        public virtual PhysicalAddress PhysicalAddress { get; set; }

        //public virtual List<UserActivity> UserActivities { get; set; } = new();
        //public virtual List<SocialLink> SocialLinks { get; set; } = new();

        // Add this navigation property
        public virtual ICollection<IdentityUserRole<string>> IdentityUserRoles { get; set; }
        public virtual ICollection<IdentityRole> IdentityRoles { get; set; }
        // If you need just the role names as a computed property
        [NotMapped]
        public List<string> UserRoles => IdentityRoles?.Select(r => r.Name).ToList() ?? new List<string>();
    }
}
