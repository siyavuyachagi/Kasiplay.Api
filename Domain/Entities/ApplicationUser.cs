using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string LastName { get; set; } = new string("");
        public string FirstName { get; set; } = new string("");
        public string Gender { get; set; } = new string("Not specified");
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Avatar { get; set; }

        //Audit/System metadata
        public bool IsDeleted { get; set; } = false; //Soft delete
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual string? Discriminator { get; set; }

        public Guid? PhysicalAddressId { get; set; }
        [ForeignKey(nameof(PhysicalAddressId))]
        public virtual PhysicalAddress PhysicalAddress { get; set; }

        // Add this navigation property
        public virtual ICollection<IdentityUserRole<string>> IdentityUserRoles { get; set; }
        public virtual ICollection<IdentityRole> IdentityRoles { get; set; }
        // If you need just the role names as a computed property
        [NotMapped]
        public List<string> UserRoles => IdentityRoles?.Select(r => r.Name).ToList() ?? new List<string>();
    }
}
